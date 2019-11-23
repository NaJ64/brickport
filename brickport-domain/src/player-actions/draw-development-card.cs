using System;

namespace BrickPort.Domain.PlayerActions
{
    public class DrawDevelopmentCard : PlayerAction, IBuildAction
    {
        public DrawDevelopmentCard(PlayerColor playerColor, DevelopmentCardType cardType) 
            : base(Guid.NewGuid(), playerColor) { }

        public DrawDevelopmentCard(Guid id, PlayerColor playerColor, DevelopmentCardType cardType) 
            : base(id, playerColor) { }
    }
}