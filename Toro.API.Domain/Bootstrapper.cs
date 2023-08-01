using Microsoft.Extensions.DependencyInjection;
using System;

namespace Toro.API.Domain
{
    public static class Bootstrapper
    {
        public static void ConfigureDomain(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            // Domain - Commands
            //services.AddScoped<IDomainNotificationContext, DomainNotificationContext>();

            //services.AddScoped<IRequestHandler<CreateUserCommand, GenericCommandResult>, CreateUserCommandHandler>();
        }
    }
}
