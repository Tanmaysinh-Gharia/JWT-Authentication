using JWTAuth.Bussiness.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTAuth.Core.Helpers;
using Microsoft.AspNetCore.Http;
using JWTAuth.Data.Repositories;
using JWTAuth.Data.Repositories.UserRepo;
using JWTAuth.Data.Repositories.RefreshTokenRepo;
using JWTAuth.Bussiness.DTOs;
using JWTAuth.Data.Entities;
using JWTAuth.Core.Helpers;
namespace JWTAuth.Bussiness.Authentication
{
    public class AuthManager : IAuthManager
    {

        private readonly JwtGenerator _jwt;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepo;
        private readonly IRefreshTokenRepo _refreshTokenRepo;
        private readonly Hashing _hashing;

        public AuthManager(Hashing hashing,JwtGenerator jwt, IHttpContextAccessor httpContextAccessor, IUserRepository userRepo, IRefreshTokenRepo refreshTokenRepo)
        {
            _hashing = hashing;
            _jwt = jwt;
            _httpContextAccessor = httpContextAccessor;
            _userRepo = userRepo;
            _refreshTokenRepo = refreshTokenRepo;
        }

        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            User user = await _userRepo.GetUserByEmailAsync(email);

            
            if (user == null || !_hashing.VerifyPassword(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException();
            }

            string ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            var accessToken = _jwt.GenerateAccessToken(email, user.Id.ToString(), ip);
            
            RefreshToken refreshToken = new RefreshToken
            {
                Token = _jwt.GenerateRefreshToken(),
                Expires= DateTime.Now.AddDays(7),
                CreatedFromIp = ip,
                IsRevoked = false,
                UserId = user.Id
            };

            await _refreshTokenRepo.SaveTokenAsync(user.Id, refreshToken);
            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
            };
        }

        public async Task<AuthResponse> RefreshTokenAsync(string token)
        { 
            var refreshToken = await _refreshTokenRepo.GetValidTokenAsync(token);
            if (refreshToken == null)
            {
                throw new UnauthorizedAccessException();
            }
            var user = await _userRepo.GetUserByIdAsync(refreshToken.UserId);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            string ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

            var accessToken = _jwt.GenerateAccessToken(user.Email, user.Id.ToString(), ip);
            refreshToken.IsRevoked = true;

            await _refreshTokenRepo.UpdateAsync(refreshToken);
            RefreshToken newRefreshToken = new RefreshToken
            {
                Token = _jwt.GenerateRefreshToken(),
                Expires = DateTime.Now.AddDays(7),
                CreatedFromIp = ip,
                IsRevoked = false,
                UserId = user.Id
            };
            await _refreshTokenRepo.SaveTokenAsync(user.Id, newRefreshToken);
            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token,
            };
        }
    }
}
