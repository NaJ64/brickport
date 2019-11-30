using System;
using Microsoft.Extensions.DependencyInjection;

namespace BrickPort.Infrastructure.Services.Domain.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBrickPortInMemoryServices(
            this IServiceCollection services, 
            Action<IBrickPortDomainServiceOptions> configureOptions = null
        ) 
        { 
            // Create new options instance
            var inMemoryServiceOptions = new BrickPortDomainServiceOptions();
            configureOptions?.Invoke(inMemoryServiceOptions);
            return services;
        }
    }
}