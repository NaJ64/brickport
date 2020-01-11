using System;
using System.Linq;
using BrickPort.Domain.Utilities;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class BuildSettlement : PlayerAction, IBuildAction, IPreGameAction
    {
        public (Tile Tile1, Tile Tile2, Tile Tile3) Location { get; }
        
        public BuildSettlement(PlayerColor playerColor, (Tile Tile1, Tile Tile2, Tile Tile3) location)
            : this(Guid.NewGuid(), playerColor, location) { }

        public BuildSettlement(Guid id, PlayerColor playerColor, (Tile Tile1, Tile Tile2, Tile Tile3) location) 
            : base(id, playerColor, pointValue: 1) => Location = location;

        public override GameState Apply(GameState gameState)
        {
            var newState = gameState.Clone();
            var player = newState.Players
                .Single(x => string.Equals(x.Color, PlayerColor.Name, StringComparison.OrdinalIgnoreCase));
            player.TotalSettlements += 1;
            player.TotalPoints += 1;
            return newState;
        }
            
        public override string ToString() =>
            $"Player {PlayerColor.Name} built settlement on tiles:  " +
            $"{Location.Tile1.ToSummary()}, " +
            $"{Location.Tile2.ToSummary()}, " +
            $"{Location.Tile3.ToSummary()}";
    }
}