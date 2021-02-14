using System;

namespace Cult.Utilities
{
    public interface IGuidGenerator
    {
        Guid NewGuid();
    }

    public  class GuidGenerator
    {
        public Guid NewGuid => Guid.NewGuid();
    }
}
