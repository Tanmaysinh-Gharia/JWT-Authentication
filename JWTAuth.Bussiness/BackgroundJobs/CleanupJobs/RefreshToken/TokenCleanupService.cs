using JWTAuth.Core.Common.Background;
using JWTAuth.Data.Repositories.RefreshTokenRepo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Bussiness.BackgroundJobs.CleanupJobs.RefreshToken
{
    public class TokenCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TokenCleanupService> _logger;
        private readonly RefreshTokenCleanupSettings _refreshTokenCleanupSettings;
        public TokenCleanupService(
            IServiceProvider serviceProvider, 
            ILogger<TokenCleanupService> logger,
            IOptions<RefreshTokenCleanupSettings> settings)
        {
            _refreshTokenCleanupSettings = settings.Value;
            _serviceProvider = serviceProvider;
            _logger = logger;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_refreshTokenCleanupSettings.CleanupEnabled)
            {
                _logger.LogInformation("Refresh token cleanup is disabled.");
                return;
            }

            _logger.LogInformation("TokenCleanupService started. Interval: {Interval} minutes", _refreshTokenCleanupSettings.CleanupIntervalMinutes);


            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var repo = scope.ServiceProvider.GetRequiredService<IRefreshTokenRepo>();
                        await repo.DeleteExpiredTokensAsync();
                        _logger.LogInformation("Expired tokens cleaned up successfully.", DateTime.Now);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while cleaning up expired tokens.");
                }

                await Task.Delay(TimeSpan.FromMinutes(_refreshTokenCleanupSettings.CleanupIntervalMinutes), stoppingToken);
            }
        }
    }
}
