namespace Cult.Guard
{
    public sealed class Guard : IGuard
    {
        public static IGuard That { get; } = new Guard();

        private Guard() { }
    }
}
