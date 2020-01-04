using System;
using System.Linq;
using BrickPort.Domain.Utilities;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class MoveRobber : TriggeredPlayerAction, ITriggeredAction
    {
        public Tile Location { get; }
        public PlayerColor StealFromPlayer { get; }
        
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
            PlayerColor stealFromPlayer = null
        ) : base(id, player, triggeredBy)
        {
            Location = location;
            StealFromPlayer = stealFromPlayer;
        }

        public override GameState Apply(GameState gameState)
        {
            var newState = gameState.Clone();
            var stealFromPlayer = StealFromPlayer == null ? null : newState.Players
                .Single(x => string.Equals(x.Color, StealFromPlayer.Color, StringComparison.OrdinalIgnoreCase));
            if (stealFromPlayer != null)
                stealFromPlayer.TotalCardsStolen += 1;
            return newState;
        }

        public override string ToString()
        {
            var description = $"Player {Player.Name} moves robber to {Location.ToSummary()}";
            if (StealFromPlayer != null)
                description += $";  Card stolen from {StealFromPlayer.Name}";
            return description;
        } 
    }
}