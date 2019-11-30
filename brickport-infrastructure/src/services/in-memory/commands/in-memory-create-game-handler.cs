using System;
using System.Threading.Tasks;
using BrickPort.Services.Commands;

namespace BrickPort.Infrastructure.Services.InMemory.Commands
{
    public class InMemoryCreateGameHandler : ICreateGameHandler
    {
        public Task<int> HandleAsync(CreateGameCommand command)
        {
            throw new NotImplementedException();
        }
    }
}