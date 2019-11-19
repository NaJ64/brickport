using System;

namespace BrickPort.Domain.PlayerActions
{
    public class UseDevelopmentCard : PlayerAction
    {
        public DevelopmentCardType DevelopmentCardType { get; }

        public UseDevelopmentCard(PlayerColor playerColor, DevelopmentCardType developmentCardType) 
            : base(Guid.NewGuid(), playerColor) => DevelopmentCardType = developmentCardType;

        public UseDevelopmentCard(Guid id, PlayerColor playerColor, DevelopmentCardType developmentCardType) 
            : base(id, playerColor) => DevelopmentCardType = developmentCardType;
    }
}