using MediatR;
// ReSharper disable All

namespace Cult.Cqrs
{
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}