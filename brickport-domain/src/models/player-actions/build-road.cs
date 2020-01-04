using System;
using System.Linq;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class BuildRoad : PlayerAction, IBuildAction
    {
        public BuildRoad(PlayerColor playerColor) 
            : this(Guid.NewGuid(), playerColor) { }

        public BuildRoad(Guid id, PlayerColor playerColor) 
            : base(id, playerColor) { }

        public override GameState Apply(GameState gameState)
        {
            var newState = gameState.Clone();
            var player = newState.Players
                .Single(x => string.Equals(x.Color, Player.Color, StringComparison.OrdinalIgnoreCase));
            player.TotalRoads += 1;
            return newState;
        }
            
        public override string ToString() => $"Player {Player.Name} built a road";
    }
}