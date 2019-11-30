using System;
using System.Collections.Generic;

namespace BrickPort.Domain.Models
{
    public class ResourceType
    {
        public string Name { get; }
        private ResourceType(string name) => Name = name;

        public static ResourceType Brick = new ResourceType(nameof(Brick));
        public static ResourceType Wood = new ResourceType(nameof(Wood));
        public static ResourceType Sheep = new ResourceType(nameof(Sheep));
        public static ResourceType Wheat = new ResourceType(nameof(Wheat));
        public static ResourceType Ore = new ResourceType(nameof(Ore));

        private static readonly List<ResourceType> _list = new List<ResourceType> 
        {
            Brick, Wood, Sheep, Wheat, Ore
        };

        public static IReadOnlyList<ResourceType> List() => _list;

        public static bool operator ==(ResourceType left, ResourceType right)
        {
            if (left is null)   
                return right is null;
            return left.Equals(right);
        }
        public static bool operator !=(ResourceType left, ResourceType right) => !(left == right);
        public override bool Equals(object obj) => (obj is ResourceType other) && Equals(other);
        public bool Equals(ResourceType other)
        {
            if (Object.ReferenceEquals(this, other))
                return true;
            if (other is null)
                return false;
            return string.Equals(Name, other.Name);
        }
        public override int GetHashCode() => Name.GetHashCode();
        public override string ToString() => Name.ToString();
    }
}