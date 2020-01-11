using System.Collections.Generic;
using System.Threading.Tasks;
using BrickPort.Services.Queries;

namespace BrickPort.Infrastructure.Services.Domain.Queries
{
    public class DomainPlayerQueries : IPlayerQueries
    {
        public Task<IEnumerable<Player>> GetAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Player> GetAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}