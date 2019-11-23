using System;

namespace BrickPort.Domain.PlayerActions
{
    public class MoveRobber : TriggeredPlayerAction, ITriggeredAction
    {
        public Tile Location { get; }
        public PlayerColor StealFromPlayer { get; }
        
        public MoveRobber(
            PlayerColor player, 
            ITriggeredAction triggeredBy,
            Tile location, 
            PlayerColor stealFromPlayer
        ) : this(Guid.NewGuid(), player, triggeredBy, location, stealFromPlayer) { }

        public MoveRobber(
            Guid id, 
            PlayerColor player, 
            ITriggeredAction triggeredBy,
            Tile location, 
            PlayerColor stealFromPlayer
        ) : base(id, player, triggeredBy)
        {
            Location = location;
            StealFromPlayer = stealFromPlayer;
        }
    }
}