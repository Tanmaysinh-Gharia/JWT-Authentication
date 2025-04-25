using JWTAuth.Bussiness.Interfaces;
using JWTAuth.Core.DependencyInjection;
using JWTAuth.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JWTAuth.Bussiness.Authentication;

namespace JWTAuth.Bussiness
{
    public  class DependencyInjection : IDependencyInjection
    {

        #region Public Methods
        public virtual void Register(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            // Register Authentication Services
            AddAuthenticationServices(serviceDescriptors);
        }
        public int Order => 1;
        #endregion

        #region Private Methods
        public static void AddAuthenticationServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IAuthManager, AuthManager>();
        }
        #endregion

    }
}
