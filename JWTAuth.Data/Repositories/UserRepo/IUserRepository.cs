using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTAuth.Data.Entities;
namespace JWTAuth.Data.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<User> GetUserByIdAsync(int id);
    }
}
