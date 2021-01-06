using System.Threading.Tasks;
// ReSharper disable All

namespace Cult.DomainDrivenDesign
{
    public interface IDomainEventManager
    {
        Task Publish(IDomainEvent @event);
        Task Publish(params IDomainEvent[] events);
    }
}
