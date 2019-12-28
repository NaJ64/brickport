using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrickPort.Services.Queries;

namespace BrickPort.Infrastructure.Services.InMemory
{
    public class InMemoryDataStore
    {
        private readonly List<GameSummary> _games;
        private readonly Dictionary<string, string> _playerNames;
        private readonly Dictionary<string, string> _playerIds;

        public InMemoryDataStore() 
        {
            _games = CreateGames();
            _playerIds = new Dictionary<string, string>();
            _playerNames = new Dictionary<string, string>();
        }
        
        public IReadOnlyCollection<GameSummary> Games => _games;
        public IReadOnlyCollection<(string, string)> Players => _playerIds.Select(x => (x.Key, x.Value)).ToList();

        public void AddNewGame(GameSummary gameSummary) => _games.Add(gameSummary);

        public string GetPlayerName(string playerId) 
        {
            if (!_playerIds.ContainsKey(playerId))
                return null;
                //throw new KeyNotFoundException($"Could not locate player with id {playerId}");
            return _playerIds[playerId];
        }

        public string GetPlayerId(string playerName)
        {
            if (!_playerNames.ContainsValue(playerName))
                return null;
                //throw new KeyNotFoundException($"Could not locate player with name {playerName}");
            return _playerNames[playerName];
        }

        public string AddNewPlayer(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName)) 
                throw new ArgumentNullException(nameof(playerName), "Player name must be provided");
            if (_playerNames.ContainsKey(playerName))
                throw new ArgumentException($"Player with name {playerName} already exists");
            var playerId = Guid.NewGuid().ToString();
            _playerIds[playerId] = playerName;
            _playerNames[playerName] = playerId;
            return playerId;
        }

        private List<GameSummary> CreateGames() 
        {
            var drewBrees = (PlayerId: Guid.NewGuid().ToString(), PlayerName: "Drew Brees");
            var thomasMorstead = (PlayerId: Guid.NewGuid().ToString(), PlayerName: "Thomas Morstead");
            var pierreThomas = (PlayerId: Guid.NewGuid().ToString(), PlayerName: "Pierre Thomas");
            var marquesColston = (PlayerId: Guid.NewGuid().ToString(), PlayerName: "Marques Colston");
            var reggieBush = (PlayerId: Guid.NewGuid().ToString(), PlayerName: "Reggie Bush");

            return new List<GameSummary>
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

    }
}