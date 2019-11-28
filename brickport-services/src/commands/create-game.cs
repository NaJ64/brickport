using System;

namespace BrickPort.Services.Commands
{
    public class CreateGameCommand : ICommand 
    { 
        DateTime DateUtc { get; set; }
    }
    public interface ICreateGameHandler : ICommandHandler<CreateGameCommand> { }
}
