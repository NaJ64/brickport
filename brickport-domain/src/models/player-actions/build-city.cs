using System;
using System.Linq;
using BrickPort.Domain.Utilities;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class BuildCity : PlayerAction, IBuildAction
    {
        public (Tile Tile1, Tile Tile2, Tile Tile3) Location { get; }
        
        public BuildCity(PlayerColor playerColor, (Tile Tile1, Tile Tile2, Tile Tile3) location, bool specialBuildPhase = false) 
            : this(Guid.NewGuid(), playerColor, location) { }

        public BuildCity(Guid id, PlayerColor playerColor, (Tile Tile1, Tile Tile2, Tile Tile3) location, bool specialBuildPhase = false) 
            : base(id, playerColor, pointValue: 1) => Location = location;

        public override GameState Apply(GameState gameState)
        {
            var newState = gameState.Clone();
            var player = newState.Players
                .Single(x => string.Equals(x.Color, PlayerColor.Name, StringComparison.OrdinalIgnoreCase));
            player.TotalCities += 1;
            player.TotalSettlements -= 1;
            player.TotalPoints += 1;
            return newState;
        }

        public override string ToString() =>
            $"Player {PlayerColor.Name} built city on tiles:  " +
            $"{Location.Tile1.ToSummary()}, " +
            $"{Location.Tile2.ToSummary()}, " +
            $"{Location.Tile3.ToSummary()}";
    }
}