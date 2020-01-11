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
                options.UseInMemoryCommands = webOptions.UseInMemoryCommands;
                options.UseInMemoryQueries = webOptions.UseInMemoryQueries;
                options.UseDomainCommands = webOptions.UseDomainCommands;
                options.UseDomainQueries = webOptions.UseDomainQueries;
            });
            return services;
        }
    }
}