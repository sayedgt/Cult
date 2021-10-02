using Microsoft.Extensions.DependencyInjection;
// ReSharper disable All

namespace Cult.TestToolkit
{
    public static class TestToolkitDependencyInjection
    {
        public static IServiceCollection AddClockService(
            this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton(typeof(IClock), typeof(Clock));
        }

        public static IServiceCollection AddGuidGeneratorService(
            this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton(typeof(IGuidGenerator), typeof(GuidGenerator));
        }
    }
}