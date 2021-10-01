using System;

namespace Cult.Toolkit
{
    public interface IGuidGenerator
    {
        Guid NewGuid();
    }

    public class GuidGenerator
    {
        public Guid NewGuid() => Guid.NewGuid();
    }
}
