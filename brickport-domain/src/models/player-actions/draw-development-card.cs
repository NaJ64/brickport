using System;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class DrawDevelopmentCard : PlayerAction, IBuildAction
    {
        public DrawDevelopmentCard(PlayerColor playerColor, DevelopmentCardType cardType) 
            : base(Guid.NewGuid(), playerColor) { }

        public DrawDevelopmentCard(Guid id, PlayerColor playerColor, DevelopmentCardType cardType) 
            : base(id, playerColor) { }
            
        public override string ToString() => $"Player {Player.Name} purchased a development card";
    }
}