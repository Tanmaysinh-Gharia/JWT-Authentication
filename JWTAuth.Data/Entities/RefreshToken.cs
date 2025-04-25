using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Data.Entities
{
    public class RefreshToken
    {
        [Key]
        public int RefreshTokenId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public int UserId { get; set; }

        public string CreatedFromIp { get; set; }
        public User User { get; set; }
    }
}
