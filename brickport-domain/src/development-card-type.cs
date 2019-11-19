using System;
using System.Linq;
using System.Collections.Generic;

namespace BrickPort.Domain
{
    public class DevelopmentCardType 
    {
        public string Name { get; }
        public string Description { get; }
        public virtual int PointValue { get; }
        public int MaxPerTurn { get; }

        private DevelopmentCardType(string name, string description, int pointValue = 0, int maxPerTurn = 1) 
        { 
            Name = name;
            Description = description;
            PointValue = pointValue;
            MaxPerTurn = maxPerTurn;
        }
        
        public static DevelopmentCardType Progress = new DevelopmentCardType("Progress", "Worth one victory point", pointValue: 1, maxPerTurn: 99);
        public static DevelopmentCardType Knight = new DevelopmentCardType("Knight", "Move the robber", maxPerTurn: 1);
        public static DevelopmentCardType Monopoly = new DevelopmentCardType("Monopoly", "Steal all of one kind of resource");
        public static DevelopmentCardType YearOfPlenty = new DevelopmentCardType("Year of Plenty", "Take any two resources from the bank");
        public static DevelopmentCardType RoadBuilder = new DevelopmentCardType("Road Builder", "Place two roads");

        private static readonly List<DevelopmentCardType> _list = new List<DevelopmentCardType>() 
        {
            Progress, Knight, Monopoly, YearOfPlenty, RoadBuilder
        };

        public static IReadOnlyList<DevelopmentCardType> List() => _list;

        public static DevelopmentCardType Unknown = new UnknownDevelopmentCardType(_list);

        class UnknownDevelopmentCardType : DevelopmentCardType 
        {
            private IEnumerable<DevelopmentCardType> _possibleTypes { get; }

            public UnknownDevelopmentCardType(IEnumerable<DevelopmentCardType> possibleTypes) 
                : base("Unknown", "Card type has not yet been revealed")
            {
                _possibleTypes = possibleTypes.Where(x => !x.Equals(this));
            }

            public override int PointValue => _possibleTypes.Max(x => x.PointValue);
        }

        public static bool operator ==(DevelopmentCardType left, DevelopmentCardType right)
        {
            if (left is null)   
                return right is null;
            return left.Equals(right);
        }
        public static bool operator !=(DevelopmentCardType left, DevelopmentCardType right) => !(left == right);
        public override bool Equals(object obj) => (obj is DevelopmentCardType other) && Equals(other);
        public bool Equals(DevelopmentCardType other)
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