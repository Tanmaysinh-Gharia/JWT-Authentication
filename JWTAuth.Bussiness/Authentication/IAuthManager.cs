using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTAuth.Bussiness.DTOs;
namespace JWTAuth.Bussiness.Interfaces
{
    public interface IAuthManager
    {
        Task<AuthResponse> LoginAsync(string email,string password);
        Task<AuthResponse> RefreshTokenAsync(string refreshToken);
    }
}
