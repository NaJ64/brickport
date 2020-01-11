using System;

namespace BrickPort.Services.Commands
{
    public class RunTestCommand : ICommand 
    { 
        public DateTime DateUtc { get; set; }
    }
    public interface IRunTestHandler : ICommandHandler<RunTestCommand> { }
}
