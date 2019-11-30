using System;

namespace BrickPort.Domain.Models.PlayerActions
{
    public class BuildInitialSettlement : BuildSettlement, IPreGameAction
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

        public override string ToString()
        {
            var summary = base.ToString();
            if (!AwardResources || (Location.Tile1 == null && Location.Tile2 == null && Location.Tile3 == null))
                return summary;
            summary += ";  Collected initial resources: ";
            if (Location.Tile1 != null) 
                summary += $"{Location.Tile1.ResourceType.Name}";
            if (Location.Tile2 != null) 
            {
                if (Location.Tile1 != null)
                    summary += ", ";
                summary += $"{Location.Tile2.ResourceType.Name}";
            }
            if (Location.Tile3 != null)
            {
                if (Location.Tile1 != null || Location.Tile2 != null)
                    summary += ", ";
                summary += $"{Location.Tile3.ResourceType.Name}";
            }
            return summary;
        }
    }
}