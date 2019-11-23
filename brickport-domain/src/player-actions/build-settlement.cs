using System;

namespace BrickPort.Domain.PlayerActions
{
    public class BuildSettlement : PlayerAction, IBuildAction
    {
        public (Tile Tile1, Tile Tile2, Tile Tile3) Location { get; }
        
        public BuildSettlement(PlayerColor playerColor, (Tile Tile1, Tile Tile2, Tile Tile3) location)
            : this(Guid.NewGuid(), playerColor, location) { }

        public BuildSettlement(Guid id, PlayerColor playerColor, (Tile Tile1, Tile Tile2, Tile Tile3) location) 
            : base(id, playerColor) => Location = location;
    }
}