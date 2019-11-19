using System;

namespace BrickPort.Domain.PlayerActions
{
    public class BuildCity : PlayerAction
    {
        public (Tile Tile1, Tile Tile2, Tile Tile3) Location { get; }
        
        public BuildCity(PlayerColor playerColor, (Tile Tile1, Tile Tile2, Tile Tile3) location, bool specialBuildPhase = false) 
            : this(Guid.NewGuid(), playerColor, location, specialBuildPhase) { }

        public BuildCity(Guid id, PlayerColor playerColor, (Tile Tile1, Tile Tile2, Tile Tile3) location, bool specialBuildPhase = false) 
            : base(id, playerColor, specialBuildPhase) => Location = location;
    }
}