using System;
using System.Linq;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class DrawDevelopmentCard : PlayerAction, IBuildAction
    {
        public DrawDevelopmentCard(PlayerColor playerColor, DevelopmentCardType cardType) 
            : base(Guid.NewGuid(), playerColor) { }

        public DrawDevelopmentCard(Guid id, PlayerColor playerColor, DevelopmentCardType cardType) 
            : base(id, playerColor) { }

        public override GameState Apply(GameState gameState)
        {
            var newState = gameState.Clone();
            var player = newState.Players
                .Single(x => string.Equals(x.Color, PlayerColor.Name, StringComparison.OrdinalIgnoreCase));
            player.TotalDevelopmentCards += 1;
            return newState;
        }
            
        public override string ToString() => $"Player {PlayerColor.Name} purchased a development card";
    }
}