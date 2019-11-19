using System;

namespace BrickPort.Domain.PlayerActions
{
    public class DrawDevelopmentCard : PlayerAction
    {
        public DrawDevelopmentCard(PlayerColor playerColor, DevelopmentCardType developmentCardType, bool specialBuildPhase = false) 
            : base(Guid.NewGuid(), playerColor, specialBuildPhase) { }

        public DrawDevelopmentCard(Guid id, PlayerColor playerColor, DevelopmentCardType developmentCardType, bool specialBuildPhase = false) 
            : base(id, playerColor, specialBuildPhase) { }
    }
}