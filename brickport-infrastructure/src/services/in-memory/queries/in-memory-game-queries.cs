using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrickPort.Services.Queries;

namespace BrickPort.Infrastructure.Services.InMemory.Queries
{
    public class InMemoryGameQueries : IGameQueries
    {
        public async Task<IEnumerable<GameSummary>> SummaryAsync()
        {
            return await Task.FromResult(new GameSummary[0]);
        }

        public Task<GameSummary> SummaryAsync(string id)
        {
            throw new KeyNotFoundException($"Could not find game with id {id}");
        }

        public async Task<IEnumerable<GameSummary>> SummaryByDateAsync(DateTime? startUtc, DateTime? endUtc)
        {
            return await SummaryAsync();
        }
    }
}