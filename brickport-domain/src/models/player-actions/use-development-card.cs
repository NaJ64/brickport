using System;
using System.Linq;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class UseDevelopmentCard : PlayerAction, IUseDevelopmentCardAction
    {
        public DevelopmentCardType CardType { get; }

        public UseDevelopmentCard(PlayerColor playerColor, DevelopmentCardType developmentCardType) 
            : base(Guid.NewGuid(), playerColor) => CardType = developmentCardType;

        public UseDevelopmentCard(Guid id, PlayerColor playerColor, DevelopmentCardType developmentCardType) 
            : base(id, playerColor) => CardType = developmentCardType;
            
        public override GameState Apply(GameState gameState) 
        {
            var newState = gameState.Clone();
            var player = newState.Players
                .Single(x => string.Equals(x.Color, PlayerColor.Name, StringComparison.OrdinalIgnoreCase));
            player.RevealedDevelopmentCards.Add(CardType.Name);
            if (CardType.PointValue > 0)
                player.TotalPoints += CardType.PointValue;
            return newState;
        }
            
        public override string ToString() => $"Player {PlayerColor.Name} plays development card ({CardType.Name})";
    }
}