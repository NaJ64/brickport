using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrickPort.Services.Queries;

namespace BrickPort.Infrastructure.Services.Domain.Queries
{
    public class DomainGameQueries : IGameQueries
    {
        public Task<IEnumerable<GameSummary>> SummaryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GameSummary> SummaryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GameSummary>> SummaryByDateAsync(DateTime? startUtc, DateTime? endUtc)
        {
            throw new NotImplementedException();
        }
    }
}