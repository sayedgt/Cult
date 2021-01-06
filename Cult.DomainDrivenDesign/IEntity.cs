using System.Collections.Generic;
// ReSharper disable All
namespace Cult.DomainDrivenDesign
{
    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
