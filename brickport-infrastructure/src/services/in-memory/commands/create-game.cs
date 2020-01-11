using System;
using System.Linq;
using System.Threading.Tasks;
using BrickPort.Services.Commands;
using BrickPort.Services.Queries;

namespace BrickPort.Infrastructure.Services.InMemory.Commands
{
    public class InMemoryCreateGameHandler : ICreateGameHandler
    {
        private readonly InMemoryDataStore _dataStore;

        public InMemoryCreateGameHandler(InMemoryDataStore dataStore) => _dataStore = dataStore;

        public Task<string> HandleAsync(CreateGameCommand command)
        {
            if (command.Players == null)
                throw new ArgumentNullException(nameof(command.Players), "Player info is required");
            if (command.Players.Length < 3 || command.Players.Length > 6)
                throw new ArgumentOutOfRangeException(nameof(command.Players), "Player count must be between 3 and 6");
            if (command.Players.GroupBy(x => x.PlayerName).Any(x => x.Count() > 1))
                throw new ArgumentException(nameof(command.Players), "Players must be unique");
            if (command.Players.GroupBy(x => x.Color).Any(x => x.Count() > 1))
                throw new ArgumentException(nameof(command.Players), "Player colors must be unique");
            if (command.Players.GroupBy(x => x.Color).Any(x => !_dataStore.ValidColors.Contains(x.Key)))
                throw new ArgumentException(nameof(command.Players), $"Player colors must be: {string.Join(", ", _dataStore.ValidColors)}");
            var newGameSummary = new GameSummary()
            {
                Id = Guid.NewGuid().ToString(),
                DateUtc = command.DateUtc,
                Winner = null,
                PlayerScores = command.Players.Select(x => new PlayerScoreSummary()
                {
                    PlayerId = _dataStore.GetPlayerId(x.PlayerName) ?? _dataStore.AddNewPlayer(x.PlayerName),
                    PlayerName = x.PlayerName,
                    Color = x.Color,
                    VictoryPoints = 2
                }).ToArray()
            };
            _dataStore.AddNewGame(newGameSummary);
            return Task.FromResult(newGameSummary.Id);
        }
    }
}