using BrickPort.Domain.Models;

namespace BrickPort.Domain.Utilities
{
    public static class ModelExtensions
    {
        public static string ToSummary(this Tile tile)
        {
            if (tile == null)
                return "Coast/Desert";
            return $"{tile.ResourceType.Name ?? "none"} ({tile.Number})";
        }
    }
}