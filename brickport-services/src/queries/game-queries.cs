using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrickPort.Services.Queries
{
    public class GameSummary
    {
        public string Id { get; set; }
        public DateTime DateUtc { get; set; }
        public string Winner { get; set; }
        public PlayerScoreSummary[] PlayerScores { get; set; }
    }

    public class PlayerScoreSummary 
    {
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Color { get; set; }
        public int VictoryPoints { get; set; }
    }

    public interface IGameQueries
    {
        Task<IEnumerable<GameSummary>> SummaryAsync();
        Task<GameSummary> SummaryAsync(string id);
        Task<IEnumerable<GameSummary>> SummaryByDateAsync(DateTime? startUtc, DateTime? endUtc);
    }
}
