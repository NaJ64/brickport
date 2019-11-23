using System;

namespace BrickPort.Domain.PlayerActions
{
    public class RollDice : PlayerAction, IRollAction
    {
        public RollResult RollResult { get; }
        
        public RollDice(PlayerColor playerColor, RollResult rollResult) 
            : this(Guid.NewGuid(), playerColor, rollResult) { }

        public RollDice(Guid id, PlayerColor playerColor, RollResult rollResult) 
            : base(id, playerColor) => RollResult = rollResult;
    }
}