// ReSharper disable All
namespace Cult.Extensions.Guard
{
    public sealed class Guard : IGuard
    {
        public static IGuard Against { get; } = new Guard();

        private Guard() { }
    }
}
