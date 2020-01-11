using System;
using System.Linq;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class LargestArmy : NewLeaderAction
    {
        public LargestArmy(
            Guid id, 
            PlayerColor player, 
            IPlayerAction triggeredBy, 
            int? newMax, 
            PlayerColor fromPlayer = null, 
            int? oldMax = null
        ) : base(id, player, 2, triggeredBy, newMax, fromPlayer, oldMax) { }
        
        public override GameState Apply(GameState gameState)
        {
            var newState = gameState.Clone();
            var fromPlayer = FromPlayerColor == null ? null : newState.Players
                .Single(x => string.Equals(x.Color, FromPlayerColor.Color, StringComparison.OrdinalIgnoreCase));
            if (fromPlayer != null)
            {
                var lostPoints = fromPlayer.HasLargestArmy ? 2 : 0;
                fromPlayer.HasLargestArmy = false;
                fromPlayer.TotalPoints -= lostPoints;
            }
            var toPlayer = PlayerColor == null ? null : newState.Players
                .Single(x => string.Equals(x.Color, PlayerColor.Name, StringComparison.OrdinalIgnoreCase));
            if (toPlayer != null)
            {
                var gainPoints = toPlayer.HasLargestArmy ? 0 : 2;
                toPlayer.HasLargestArmy = true;
                toPlayer.TotalPoints += gainPoints;
            }
            return newState;
        }
        
        public override string ToString()
        {
            var description = $"Player {PlayerColor.Name} awarded {PointValue} point(s) for gaining Largest Army ({NewMax})";
            if (FromPlayerColor == null)
                return description;
            return description += $";  Previous leader {FromPlayerColor} ({OldMax}) loses {PointValue} point(s)";
        } 
    }
}