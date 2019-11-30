using System;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class RollDice : PlayerAction, IRollAction
    {
        public RollResult RollResult { get; }
        
        public RollDice(PlayerColor playerColor, RollResult rollResult) 
            : this(Guid.NewGuid(), playerColor, rollResult) { }

        public RollDice(Guid id, PlayerColor playerColor, RollResult rollResult) 
            : base(id, playerColor) => RollResult = rollResult;

        public override string ToString() =>
            $"Player {Player.Name} rolls {RollResult.Value} ({RollResult.Die1} + {RollResult.Die2})";
    }
}