
using JWTAuth.Core.DependencyInjection;
using JWTAuth.Data;
using JWTAuth.Data.Repositories.UserRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JWTAuth.Data.Repositories.RefreshTokenRepo;
namespace JWTAuth.Data
{
    public  class DependencyInjection : IDependencyInjection
    {

        #region Public Methods
        public virtual void Register(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            // Add DbContext
            serviceDescriptors.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Injection of Repository Services 

            // Injection : User Repo 
            AddUserRepository(serviceDescriptors);

            // Injection : Refresh Token Repo
            AddRefreshTokenRepository(serviceDescriptors);


        }
        public int Order => 2;
        #endregion

        #region Private Methods
        private static void AddUserRepository(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IUserRepository,UserRepository>();
        }

        private static void AddRefreshTokenRepository(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
        }
        #endregion

    }
}
