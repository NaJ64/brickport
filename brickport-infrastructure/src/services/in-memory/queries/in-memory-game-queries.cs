using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrickPort.Services.Queries;

namespace BrickPort.Infrastructure.Services.InMemory.Queries
{
    public class InMemoryGameQueries : IGameQueries
    {
        private readonly List<GameSummary> _gameSummaries;

        public InMemoryGameQueries()
        {
            var drewBrees = (PlayerId: Guid.NewGuid().ToString(), PlayerName: "Drew Brees");
            var thomasMorstead = (PlayerId: Guid.NewGuid().ToString(), PlayerName: "Thomas Morstead");
            var pierreThomas = (PlayerId: Guid.NewGuid().ToString(), PlayerName: "Pierre Thomas");
            var marquesColston = (PlayerId: Guid.NewGuid().ToString(), PlayerName: "Marques Colston");
            var reggieBush = (PlayerId: Guid.NewGuid().ToString(), PlayerName: "Reggie Bush");

            _gameSummaries = new List<GameSummary>
            {
                new GameSummary()
                {
                    Id = Guid.NewGuid().ToString(),
                    DateUtc = DateTime.UtcNow,
                    Winner = drewBrees.PlayerName,
                    PlayerScores = new PlayerScoreSummary[]
                    {
                        new PlayerScoreSummary()
                        {
                            PlayerId = drewBrees.PlayerId,
                            PlayerName = drewBrees.PlayerName,
                            Color = "Blue",
                            VictoryPoints = 10
                        },
                        new PlayerScoreSummary()
                        {
                            PlayerId = marquesColston.PlayerId,
                            PlayerName = marquesColston.PlayerName,
                            Color = "Orange",
                            VictoryPoints = 9
                        },
                        new PlayerScoreSummary()
                        {
                            PlayerId = pierreThomas.PlayerId,
                            PlayerName = pierreThomas.PlayerName,
                            Color = "White",
                            VictoryPoints = 7
                        },
                        new PlayerScoreSummary()
                        {
                            PlayerId = thomasMorstead.PlayerId,
                            PlayerName = thomasMorstead.PlayerName,
                            Color = "Red",
                            VictoryPoints = 6
                        }
                    }
                },
                new GameSummary()
                {
                    Id = Guid.NewGuid().ToString(),
                    DateUtc = DateTime.UtcNow.AddDays(1),
                    Winner = pierreThomas.PlayerName,
                    PlayerScores = new PlayerScoreSummary[]
                    {
                        new PlayerScoreSummary()
                        {
                            PlayerId = pierreThomas.PlayerId,
                            PlayerName = pierreThomas.PlayerName,
                            Color = "White",
                            VictoryPoints = 10
                        },
                        new PlayerScoreSummary()
                        {
                            PlayerId = drewBrees.PlayerId,
                            PlayerName = drewBrees.PlayerName,
                            Color = "Blue",
                            VictoryPoints = 8
                        },
                        new PlayerScoreSummary()
                        {
                            PlayerId = reggieBush.PlayerId,
                            PlayerName = reggieBush.PlayerName,
                            Color = "Orange",
                            VictoryPoints = 7
                        }
                    }
                }
            };
        }

        public async Task<IEnumerable<GameSummary>> SummaryAsync() => await Task.FromResult(_gameSummaries);

        public async Task<GameSummary> SummaryAsync(string id) => await Task.FromResult(_gameSummaries.SingleOrDefault(summary => summary.Id == id));

        public async Task<IEnumerable<GameSummary>> SummaryByDateAsync(DateTime? startUtc, DateTime? endUtc) => await Task.FromResult(
            _gameSummaries.Where(summary => 
                summary.DateUtc >= (startUtc ?? DateTime.MinValue) && 
                summary.DateUtc <= (endUtc ?? DateTime.MaxValue)
            )
        );
    }
}