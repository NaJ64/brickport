using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrickPort.Services.Queries;

namespace BrickPort.Infrastructure.Services.InMemory.Queries
{
    public class InMemoryGameQueries : IGameQueries
    {
        private readonly InMemoryDataStore _dataStore;

        public InMemoryGameQueries(InMemoryDataStore dataStore) => _dataStore = dataStore;

        public async Task<IEnumerable<GameSummary>> SummaryAsync() => await Task.FromResult(_dataStore.Games);

        public async Task<GameSummary> SummaryAsync(string id) => await Task.FromResult(_dataStore.Games.SingleOrDefault(summary => summary.Id == id));

        public async Task<IEnumerable<GameSummary>> SummaryByDateAsync(DateTime? startUtc, DateTime? endUtc) => await Task.FromResult(
            _dataStore.Games.Where(summary => 
                summary.DateUtc >= (startUtc ?? DateTime.MinValue) && 
                summary.DateUtc <= (endUtc ?? DateTime.MaxValue)
            )
        );
    }
}