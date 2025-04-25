using JWTAuth.Core.DependencyInjection;
using JWTAuth.Core.TypeFinders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Core
{
    public static class DependencyRegistrar
    {
        public static void RegisterDependencies(this IServiceCollection services,ITypeFinder typeFinder, IConfiguration configuration)
        {
            // find dependency registrars defined in the assembly 
            IEnumerable<Type> dependencyRegistrars = typeFinder.FindClassesOfType<IDependencyInjection>(true);
            
            //create and sort instances of dependency registrars
            IOrderedEnumerable<IDependencyInjection> instances = dependencyRegistrars.
                Select(type => (IDependencyInjection)Activator.CreateInstance(type))
                .OrderBy(instance => instance.Order);

            foreach (IDependencyInjection dependencyInjection in instances)
            {
                // Register the dependencies using the current instance
                dependencyInjection.Register(services, configuration);
            }
        }
    }
}
