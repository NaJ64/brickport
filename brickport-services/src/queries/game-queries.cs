using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrickPort.Services.Queries
{
    public class GameSummary
    {
        string Id { get; }
        DateTime DateUtc { get; }
        string Winner { get; }
        PlayerScoreSummary[] PlayerScores { get; }
    }

    public class PlayerScoreSummary 
    {
        public string PlayerId { get; }
        public string PlayerName { get; }
        public string Color { get; }
        public int VictoryPoints { get; }
    }

    public interface IGameQueries
    {
        Task<GameSummary> Summary(string id);
        Task<IEnumerable<GameSummary>> SummaryByDateAsync(DateTime? startUtc, DateTime? endUtc);
    }
}
