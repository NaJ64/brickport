using System;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class BuildRoad : PlayerAction, IBuildAction
    {
        public BuildRoad(PlayerColor playerColor) 
            : this(Guid.NewGuid(), playerColor) { }

        public BuildRoad(Guid id, PlayerColor playerColor) 
            : base(id, playerColor) { }
            
        public override string ToString() => $"Player {Player.Name} built a road";
    }
}