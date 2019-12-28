using System;

namespace BrickPort.Services.Commands
{
    public class CreateGameCommand : ICommand 
    { 
        public DateTime DateUtc { get; set; }
        public PlayerAndColor[] Players { get; set; }
    }
    public class PlayerAndColor
    {
        public string PlayerName { get; set; }
        public string Color { get; set; }
    }
    public interface ICreateGameHandler : ICommandHandler<CreateGameCommand, string> { }
}
