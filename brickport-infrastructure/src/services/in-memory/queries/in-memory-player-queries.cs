using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrickPort.Services.Queries;

namespace BrickPort.Infrastructure.Services.InMemory.Queries
{
    public class InMemoryPlayerQueries : IPlayerQueries
    {
        private readonly InMemoryDataStore _dataStore;

        public InMemoryPlayerQueries(InMemoryDataStore dataStore) => _dataStore = dataStore;

        public async Task<IEnumerable<Player>> GetAsync() => await Task.FromResult(
            _dataStore.Players.Select(x => new Player() 
            {
                PlayerId = x.Item1,
                PlayerName = x.Item2
            })
        );

        public async Task<Player> GetAsync(string id) => await Task.FromResult(
            (await GetAsync()).Single(summary => summary.PlayerId == id)
        );

    }
}