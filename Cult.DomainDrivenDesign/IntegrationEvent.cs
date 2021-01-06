using System;
// ReSharper disable All
namespace Cult.DomainDrivenDesign
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; private set; }
        public DateTimeOffset OccurredOn { get; private set; }

        protected IntegrationEvent()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTimeOffset.Now;
        }
    }
}
