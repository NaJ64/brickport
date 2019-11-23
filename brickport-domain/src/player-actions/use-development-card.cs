using System;

namespace BrickPort.Domain.PlayerActions
{
    public class UseDevelopmentCard : PlayerAction, IUseDevelopmentCardAction
    {
        public DevelopmentCardType CardType { get; }

        public UseDevelopmentCard(PlayerColor playerColor, DevelopmentCardType developmentCardType) 
            : base(Guid.NewGuid(), playerColor) => CardType = developmentCardType;

        public UseDevelopmentCard(Guid id, PlayerColor playerColor, DevelopmentCardType developmentCardType) 
            : base(id, playerColor) => CardType = developmentCardType;
    }
}