using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Core.Helpers
{
    public class Hashing
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string password, string hashedPassword)
        {
            var hasedone = HashPassword(password);
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
