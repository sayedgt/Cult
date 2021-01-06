using System.Threading.Tasks;
// ReSharper disable All

namespace Cult.Cqrs
{
    public interface ICommandQueryManager
    {
        Task Send(ICommand command);
        Task<TResponse> Send<TResponse>(ICommand<TResponse> command);
        Task<TResponse> Send<TResponse>(IQuery<TResponse> query);
    }
}
