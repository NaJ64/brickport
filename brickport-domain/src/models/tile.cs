using System;
using BrickPort.Domain.Utilities;

namespace BrickPort.Domain.Models
{
    public class Tile
    {
        public int Number { get; }
        public ResourceType ResourceType { get; }
        
        public Tile(int number, ResourceType resourceType)
        {
            Number = number;
            ResourceType = resourceType;
        }

        public override string ToString() => this.ToSummary();
    }
}
