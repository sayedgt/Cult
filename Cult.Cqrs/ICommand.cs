using MediatR;
// ReSharper disable All

namespace Cult.Cqrs
{
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
