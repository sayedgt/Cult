using MediatR;
// ReSharper disable All

namespace Cult.Cqrs
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}