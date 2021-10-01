using Microsoft.Extensions.DependencyInjection;
// ReSharper disable All

namespace Cult.SimpleCacheManager
{
    public static class SimpleCacheManager
    {
        public static IServiceCollection AddSimpleCacheManager(
            this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton(typeof(ICacheManager<,>), typeof(CacheManager<,>));
        }
    }
}