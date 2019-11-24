using System;

namespace BrickPort.Services.Commands
{
    public class CreateGame : ICommand 
    { 
        DateTime DateUtc { get; set; }
    }
    public interface ICreateGameHandler : ICommandHandler<CreateGame> { }
}
