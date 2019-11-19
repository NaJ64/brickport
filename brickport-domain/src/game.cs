using System;
using System.Collections.Generic;
using System.Linq;

namespace BrickPort.Domain
{
    public class Game
    {
        private readonly GameType _gameType;
        private readonly Dictionary<PlayerColor, Player> _players;
        private readonly GameState _initialGameState;
        private readonly Stack<PlayerTurn> _turns;

        public Guid Id { get; }

        public Game(GameType gameType, IDictionary<PlayerColor, Player> players, IEnumerable<PlayerTurn> turns = null)
        {
            _gameType = gameType;
            _initialGameState = new GameState() { DevelopmentCards = new List<DevelopmentCard>() };
            foreach (var development in _gameType.Developments)
            {
                for (var i = 0; i < development.Value; i++)
                    _initialGameState.DevelopmentCards.Add(new DevelopmentCard(development.Key));
            };
            _players = new Dictionary<PlayerColor, Player>();
            foreach(var playerColor in _gameType.PlayerColors) 
            {
                if (players.ContainsKey(playerColor) && players[playerColor] != null)
                    _players[playerColor] = players[playerColor];
            }
            _turns = new Stack<PlayerTurn>(turns?.ToList() ?? new List<PlayerTurn>());
        }

        public GameState GetState()
        {
            var gameState = _initialGameState;
            foreach(var turn in _turns) 
            {
                // TODO:  Apply action to game and get new state
            }
            return gameState;
        }

    }

    public class GameState
    {
        public IList<DevelopmentCard> DevelopmentCards { get; set; }
        // TODO:  Add player points, stats, used cards, etc
    }
}