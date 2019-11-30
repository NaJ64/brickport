using System;
using BrickPort.Domain.Utilities;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class BuildSettlement : PlayerAction, IBuildAction
    {
        public (Tile Tile1, Tile Tile2, Tile Tile3) Location { get; }
        
        public BuildSettlement(PlayerColor playerColor, (Tile Tile1, Tile Tile2, Tile Tile3) location)
            : this(Guid.NewGuid(), playerColor, location) { }

        public BuildSettlement(Guid id, PlayerColor playerColor, (Tile Tile1, Tile Tile2, Tile Tile3) location) 
            : base(id, playerColor) => Location = location;
            
        public override string ToString() =>
            $"Player {Player.Name} built settlement on tiles:  " +
            $"{Location.Tile1.ToSummary()}, " +
            $"{Location.Tile2.ToSummary()}, " +
            $"{Location.Tile3.ToSummary()}";
    }
}