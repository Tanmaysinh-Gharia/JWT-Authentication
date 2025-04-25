using JWTAuth.Bussiness.Interfaces;
using JWTAuth.Core.DependencyInjection;
using JWTAuth.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JWTAuth.Bussiness.Authentication;
using JWTAuth.Bussiness.BackgroundJobs.CleanupJobs.RefreshToken;
using JWTAuth.Core.Common.Background;

namespace JWTAuth.Bussiness
{
    public  class DependencyInjection : IDependencyInjection
    {

        #region Public Methods
        public virtual void Register(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            #region Ongoing Jobs
            // Register Authentication Services
            AddAuthenticationServices(serviceDescriptors);
            #endregion

            #region Background Jobs
            // Register Background Services
            AddRefreshTokenCleanupService(serviceDescriptors, configuration);
            #endregion
        }
        public int Order => 1;
        #endregion

        #region Private Methods
        private static void AddAuthenticationServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IAuthManager, AuthManager>();
        }

        private static void AddRefreshTokenCleanupService(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {

            serviceDescriptors.Configure<RefreshTokenCleanupSettings>(
                configuration.GetSection("BackgroundService:CleanUp:RefreshToken"));
            serviceDescriptors.AddHostedService<TokenCleanupService>();
        }
        #endregion

    }
}
