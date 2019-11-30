using System;
using BrickPort.Domain.Utilities;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class BuildCity : PlayerAction, IBuildAction
    {
        public (Tile Tile1, Tile Tile2, Tile Tile3) Location { get; }
        
        public BuildCity(PlayerColor playerColor, (Tile Tile1, Tile Tile2, Tile Tile3) location, bool specialBuildPhase = false) 
            : this(Guid.NewGuid(), playerColor, location) { }

        public BuildCity(Guid id, PlayerColor playerColor, (Tile Tile1, Tile Tile2, Tile Tile3) location, bool specialBuildPhase = false) 
            : base(id, playerColor) => Location = location;

        public override string ToString() =>
            $"Player {Player.Name} built city on tiles:  " +
            $"{Location.Tile1.ToSummary()}, " +
            $"{Location.Tile2.ToSummary()}, " +
            $"{Location.Tile3.ToSummary()}";
    }
}