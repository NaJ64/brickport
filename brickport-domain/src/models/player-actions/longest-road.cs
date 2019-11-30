using System;

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

        public override string ToString()
        {
            var description = $"Player {Player.Name} awarded {PointValue} point(s) for gaining Longest Road ({NewMax})";
            if (FromPlayer == null)
                return description;
            return description += $";  Previous leader {FromPlayer} ({OldMax}) loses {PointValue} point(s)";
        } 
    }
}