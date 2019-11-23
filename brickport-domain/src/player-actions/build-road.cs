using System;

namespace BrickPort.Domain.PlayerActions
{
    public class BuildRoad : PlayerAction, IBuildAction
    {
        public (Tile Tile1, Tile Tile2) Location { get; }
        
        public BuildRoad(PlayerColor playerColor, (Tile Tile1, Tile Tile2) location) 
            : this(Guid.NewGuid(), playerColor, location) { }

        public BuildRoad(Guid id, PlayerColor playerColor, (Tile Tile1, Tile Tile2) location) 
            : base(id, playerColor) => Location = location;
    }
}