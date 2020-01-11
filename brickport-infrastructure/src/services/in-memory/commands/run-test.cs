using System;
using System.Linq;
using System.Threading.Tasks;
using BrickPort.Services.Commands;
using BrickPort.Services.Queries;

namespace BrickPort.Infrastructure.Services.InMemory.Commands 
{
    public class InMemoryRunTestHandler : IRunTestHandler
    {
        public Task<string> HandleAsync(RunTestCommand command)
        {
            throw new NotImplementedException();
        }
    }
}