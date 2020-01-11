using System;
using System.Threading.Tasks;

namespace BrickPort.Services.Commands
{
    public interface ICommandHandler<TCommand> : ICommandHandler<TCommand, string> where TCommand : ICommand { }
    public interface ICommandHandler<TCommand, TResult>
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
