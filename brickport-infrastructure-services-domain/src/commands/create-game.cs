using System.Threading.Tasks;
using BrickPort.Services.Commands;

namespace BrickPort.Infrastructure.Services.Domain.Commands
{
    public class DomainCreateGameHandler : ICreateGameHandler
    {
        public Task<string> HandleAsync(CreateGameCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}