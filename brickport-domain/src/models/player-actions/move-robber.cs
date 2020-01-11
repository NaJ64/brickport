using System;
using System.Linq;
using BrickPort.Domain.Utilities;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class MoveRobber : TriggeredPlayerAction, ITriggeredAction
    {
        public Tile Location { get; }
        public PlayerColor StealFromPlayerColor { get; }
        
        public MoveRobber(
            PlayerColor player, 
            ITriggeredAction triggeredBy,
            Tile location, 
            PlayerColor stealFromPlayer = null
        ) : this(Guid.NewGuid(), player, triggeredBy, location, stealFromPlayer) { }

        public MoveRobber(
            Guid id, 
            PlayerColor player, 
            ITriggeredAction triggeredBy,
            Tile location, 
            PlayerColor stealFromPlayerColor = null
        ) : base(id, player, triggeredBy)
        {
            Location = location;
            StealFromPlayerColor = stealFromPlayerColor;
        }

        public override GameState Apply(GameState gameState)
        {
            var newState = gameState.Clone();
            var stealFromPlayer = StealFromPlayerColor == null ? null : newState.Players
                .Single(x => string.Equals(x.Color, StealFromPlayerColor.Name, StringComparison.OrdinalIgnoreCase));
            if (stealFromPlayer != null)
                stealFromPlayer.TotalCardsStolen += 1;
            return newState;
        }

        public override string ToString()
        {
            var description = $"Player {PlayerColor.Name} moves robber to {Location.ToSummary()}";
            if (StealFromPlayerColor != null)
                description += $";  Card stolen from {StealFromPlayerColor.Name}";
            return description;
        } 
    }
}