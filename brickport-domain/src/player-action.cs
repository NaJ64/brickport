using System;
using System.Collections.Generic;
using System.Linq;

namespace BrickPort.Domain
{
    public abstract class PlayerAction
    {
        public Guid Id { get; }
        public PlayerColor PlayerColor { get; }
        public bool SpecialBuildPhase { get; }
        public virtual string Description => this.GetType().Name;

        public PlayerAction(Guid id, PlayerColor playerColor, bool specialBuildPhase = false) 
        {
            Id = id;
            PlayerColor = playerColor;
            SpecialBuildPhase = specialBuildPhase;
        }
    }
}