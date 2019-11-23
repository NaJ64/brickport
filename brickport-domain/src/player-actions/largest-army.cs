using System;

namespace BrickPort.Domain.PlayerActions
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
    }
}