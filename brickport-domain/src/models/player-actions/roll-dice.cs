using System;
using System.Linq;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class RollDice : PlayerAction, IRollAction
    {
        public RollResult RollResult { get; }
        
        public RollDice(PlayerColor playerColor, RollResult rollResult) 
            : this(Guid.NewGuid(), playerColor, rollResult) { }

        public RollDice(Guid id, PlayerColor playerColor, RollResult rollResult) 
            : base(id, playerColor) => RollResult = rollResult;
            
        public override GameState Apply(GameState gameState)
        {
            var newState = gameState.Clone();
            var player = newState.Players
                .Single(x => string.Equals(x.Color, PlayerColor.Name, StringComparison.OrdinalIgnoreCase));
            player.RollHistory.Add(RollResult.Value);
            return newState;
        }

        public override string ToString() =>
            $"Player {PlayerColor.Name} rolls {RollResult.Value} ({RollResult.Die1} + {RollResult.Die2})";
    }
}