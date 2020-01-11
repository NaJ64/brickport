using System;
using BrickPort.Infrastructure.Services.Domain.DependencyInjection;
using BrickPort.Infrastructure.Services.InMemory.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BrickPort.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions 
    {
        public static IServiceCollection AddBrickPortInfrastructure(
            this IServiceCollection services, 
            Action<IBrickPortInfrastructureOptions> configureOptions = null
        )
        {
            // Create new options instance
            var infrastructureOptions = new BrickPortInfrastructureOptions();
            configureOptions?.Invoke(infrastructureOptions);

            // Register infrastructure options
            services.AddSingleton<IBrickPortInfrastructureOptions>(infrastructureOptions);

            // Make sure we don't register conflicting types
            if (infrastructureOptions.UseDomainQueries && infrastructureOptions.UseInMemoryQueries) 
                throw new Exception("Cannot register domain queries while in-memory queries are enabled");
            if (infrastructureOptions.UseDomainCommands && infrastructureOptions.UseInMemoryCommands) 
                throw new Exception("Cannot register domain commands while in-memory queries are enabled");

            // Domain services
            if (infrastructureOptions.UseDomainCommands || infrastructureOptions.UseDomainQueries) 
            {
                services.AddBrickPortDomainServices(options => 
                {
                    options.UseCommands = infrastructureOptions.UseDomainCommands;
                    options.UseQueries = infrastructureOptions.UseDomainQueries;
                });
            }
            
            // In-memory services
            if (infrastructureOptions.UseInMemoryCommands || infrastructureOptions.UseInMemoryQueries) 
            {
                services.AddBrickPortInMemoryServices(options => 
                {
                    options.UseCommands = infrastructureOptions.UseInMemoryCommands;
                    options.UseQueries = infrastructureOptions.UseInMemoryQueries;
                });
            }

            return services;
        }

    }
}