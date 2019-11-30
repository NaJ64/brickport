using System;
using System.Collections.Generic;

namespace BrickPort.Domain.Models
{
    public class PlayerColor
    {
        public string Name { get; }
        public string Color { get; }

        private PlayerColor(string name, string color)
        {
            Name = name;
            Color = color;
        }

        public static PlayerColor Red = new PlayerColor(nameof(Red), "#ff0000");
        public static PlayerColor White = new PlayerColor(nameof(White), "#ffffff");
        public static PlayerColor Blue = new PlayerColor(nameof(Blue), "#0000ff");
        public static PlayerColor Orange = new PlayerColor(nameof(Orange), "orange");
        public static PlayerColor Green = new PlayerColor(nameof(Green), "#00ff00");
        public static PlayerColor Brown = new PlayerColor(nameof(Brown), "brown");

        private static readonly List<PlayerColor> _list = new List<PlayerColor> 
        {
            Red, White, Blue, Orange, Green, Brown
        };

        public static IReadOnlyList<PlayerColor> List() => _list;

        public static bool operator ==(PlayerColor left, PlayerColor right)
        {
            if (left is null)   
                return right is null;
            return left.Equals(right);
        }
        public static bool operator !=(PlayerColor left, PlayerColor right) => !(left == right);
        public override bool Equals(object obj) => (obj is PlayerColor other) && Equals(other);
        public bool Equals(PlayerColor other)
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