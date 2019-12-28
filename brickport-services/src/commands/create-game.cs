using System;

namespace BrickPort.Services.Commands
{
    public class CreateGameCommand : ICommand 
    { 
        public DateTime DateUtc { get; set; }
    }
    public interface ICreateGameHandler : ICommandHandler<CreateGameCommand> { }
}
