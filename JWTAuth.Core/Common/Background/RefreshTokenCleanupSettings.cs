using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Core.Common.Background
{
    public class RefreshTokenCleanupSettings
    {
        public bool CleanupEnabled { get; set; } = true;
        public int CleanupIntervalMinutes { get; set; } = 60;
    }
}
