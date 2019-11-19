using System;

namespace BrickPort.Domain
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
    }
}
