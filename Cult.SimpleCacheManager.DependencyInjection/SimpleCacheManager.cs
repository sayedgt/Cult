using System;
using Microsoft.Extensions.DependencyInjection;

namespace Cult.SimpleCacheManager.DependencyInjection
{
    public static class SimpleCacheManager
    {
        public static IServiceCollection AddSimpleCacheManager(
            this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<ICacheManager, CacheManager>();
        }
    }
}