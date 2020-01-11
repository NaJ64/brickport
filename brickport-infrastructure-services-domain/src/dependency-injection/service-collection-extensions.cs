using System;
using BrickPort.Infrastructure.Services.Domain.Commands;
using BrickPort.Infrastructure.Services.Domain.Queries;
using BrickPort.Services.Commands;
using BrickPort.Services.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BrickPort.Infrastructure.Services.Domain.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBrickPortDomainServices(
            this IServiceCollection services, 
            Action<IBrickPortDomainServiceOptions> configureOptions = null
        ) 
        { 
            // Create new options instance
            var domainServiceOptions = new BrickPortDomainServiceOptions();
            configureOptions?.Invoke(domainServiceOptions);
            if (domainServiceOptions.UseCommands)
            {
                services.AddSingleton<ICreateGameHandler, DomainCreateGameHandler>();
                services.AddSingleton<IRunTestHandler, DomainRunTestHandler>();
            }
            if (domainServiceOptions.UseQueries)
            {
                services.AddSingleton<IGameQueries, DomainGameQueries>();
                services.AddSingleton<IPlayerQueries, DomainPlayerQueries>();
            }
            return services;
        }
    }
}