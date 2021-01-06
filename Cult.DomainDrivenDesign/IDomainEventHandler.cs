using MediatR;
// ReSharper disable All

namespace Cult.DomainDrivenDesign
{
    public interface IDomainEventHandler<in TDomainEvent> 
        : INotificationHandler<TDomainEvent> where TDomainEvent : INotification
    {
    }
}
