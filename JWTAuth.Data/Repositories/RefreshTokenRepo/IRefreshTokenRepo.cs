using JWTAuth.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Data.Repositories.RefreshTokenRepo
{
    public interface IRefreshTokenRepo
    {
        Task SaveTokenAsync(int userId, RefreshToken token);
        Task<RefreshToken> GetValidTokenAsync(string token);
        Task UpdateAsync(RefreshToken token);
        Task DeleteExpiredTokensAsync();
    }
}
