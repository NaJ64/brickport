using System;
using BrickPort.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BrickPort.Web.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBrickPortWeb(
            this IServiceCollection services, 
            Action<IBrickPortWebOptions> configureOptions = null
        ) 
        { 
            // Create new options instance
            var webOptions = new BrickPortWebOptions();
            configureOptions?.Invoke(webOptions);
            services.AddBrickPortInfrastructure(options =>
            {
                options.UseInMemoryCommands = true;
                options.UseInMemoryQueries = true;
                options.UseDomainCommands = false;
                options.UseDomainQueries = false;
            });
            return services;
        }
    }
}