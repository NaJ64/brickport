using System;
using System.Linq;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class LongestRoad : NewLeaderAction
    {
        public LongestRoad(
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
                var lostPoints = fromPlayer.HasLongestRoad ? 2 : 0;
                fromPlayer.HasLongestRoad = false;
                fromPlayer.TotalPoints -= lostPoints;
            }
            var toPlayer = PlayerColor == null ? null : newState.Players
                .Single(x => string.Equals(x.Color, PlayerColor.Name, StringComparison.OrdinalIgnoreCase));
            if (toPlayer != null)
            {
                var gainPoints = toPlayer.HasLongestRoad ? 0 : 2;
                toPlayer.HasLongestRoad = true;
                toPlayer.TotalPoints += gainPoints;
            }
            return newState;
        }

        public override string ToString()
        {
            var description = $"Player {PlayerColor.Name} awarded {PointValue} point(s) for gaining Longest Road ({NewMax})";
            if (FromPlayerColor == null)
                return description;
            return description += $";  Previous leader {FromPlayerColor} ({OldMax}) loses {PointValue} point(s)";
        } 
    }
}