using System;
using BrickPort.Infrastructure.Services.InMemory.Commands;
using BrickPort.Infrastructure.Services.InMemory.Queries;
using BrickPort.Services.Commands;
using BrickPort.Services.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BrickPort.Infrastructure.Services.InMemory.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBrickPortInMemoryServices(
            this IServiceCollection services, 
            Action<IBrickPortInMemoryServiceOptions> configureOptions = null
        ) 
        { 
            // Create new options instance
            var inMemoryServiceOptions = new BrickPortInMemoryServiceOptions();
            configureOptions?.Invoke(inMemoryServiceOptions);
            services.AddSingleton<InMemoryDataStore>();
            if (inMemoryServiceOptions.UseCommands)
            {
                services.AddSingleton<ICreateGameHandler, InMemoryCreateGameHandler>();
                services.AddSingleton<IRunTestHandler, InMemoryRunTestHandler>();
            }
            if (inMemoryServiceOptions.UseQueries)
            {
                services.AddSingleton<IGameQueries, InMemoryGameQueries>();
                services.AddSingleton<IPlayerQueries, InMemoryPlayerQueries>();
            }
            return services;
        }
    }
}