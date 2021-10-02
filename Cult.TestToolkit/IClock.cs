using System;
// ReSharper disable UnusedMember.Global

namespace Cult.TestToolkit
{
    public interface IClock
    {
        DateTime Now { get; }
        DateTimeOffset OffsetNow { get; }
        DateTimeOffset OffsetUtcNow { get; }
        DateTime UtcNow { get; }
    }
}