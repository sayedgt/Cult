using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
// ReSharper disable All

namespace Cult.DryIoc
{
    public static class DryIocExtensions
    {
        public static IHostBuilder UseDryIoc(this IHostBuilder hostBuilder, IServiceProviderFactory<IContainer> factory = null)
        {
            return hostBuilder.UseServiceProviderFactory(factory ?? new DryIocServiceProviderFactory());
        }
    }
}
