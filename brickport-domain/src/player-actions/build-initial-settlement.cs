using System;

namespace BrickPort.Domain.PlayerActions
{
    public class BuildInitialSettlement : BuildSettlement
    {
        public int Turn { get; }
        public bool AwardResources { get; }

        public BuildInitialSettlement(
            PlayerColor playerColor,
            (Tile Tile1, Tile Tile2, Tile Tile3) location, 
            int turn, 
            bool awardResources
        ) : this(Guid.NewGuid(), playerColor, location, turn, awardResources) { }
        
        public BuildInitialSettlement(
            Guid id,
            PlayerColor playerColor,
            (Tile Tile1, Tile Tile2, Tile Tile3) location, 
            int turn, 
            bool awardResources
        ) : base(id, playerColor, location)
        {
            Turn = turn;
            AwardResources = awardResources;
        }
    }
}