using Microsoft.Extensions.DependencyInjection;
using Toro.API.Domain.Repositories;
using Toro.API.Domain.Services;
using Toro.API.Infrastructure.Repositories;
using Toro.API.Infrastructure.Services;

namespace Toro.API.Infrastructure
{
    public static class Bootstrapper
    {
        public static void ConfigureInfrastructure(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<MongoDBContext>();
            services.AddScoped<SecurityIdentityBase, SecurityService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();


            RegisterMappings.Register();
        }
    }
}
