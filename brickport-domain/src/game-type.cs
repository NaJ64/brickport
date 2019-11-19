using System.Collections.Generic;
using System.Linq;

namespace BrickPort.Domain
{
    public class GameType
    {
        public string Name { get; }
        public int MinPlayers { get; }
        public int MaxPlayers { get; }
        public IReadOnlyList<PlayerColor> PlayerColors { get; }
        public IDictionary<ResourceType, int> Resources { get; }
        public IDictionary<DevelopmentCardType, int> Developments { get; }

        private GameType(
            string name, 
            int minPlayers, int maxPlayers, 
            IEnumerable<PlayerColor> playerColors, 
            Dictionary<ResourceType, int> resources,
             Dictionary<DevelopmentCardType, int> developments)
        {
            Name = name;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            PlayerColors = playerColors.ToList();
            Resources = resources;
            Developments = developments;
        }

        public static GameType Standard = new GameType(
            "Standard", 
            3, 4,
            new List<PlayerColor> {
                PlayerColor.Red, 
                PlayerColor.White, 
                PlayerColor.Blue, 
                PlayerColor.Orange
            },
            new Dictionary<ResourceType, int>()
            {
                { ResourceType.Brick, 19 },
                { ResourceType.Wood, 19 },
                { ResourceType.Sheep, 19 },
                { ResourceType.Wheat, 19 },
                { ResourceType.Ore, 19 }
            },
            new Dictionary<DevelopmentCardType, int>()
            {
                { DevelopmentCardType.Knight, 14 },
                { DevelopmentCardType.Progress, 5 },
                { DevelopmentCardType.Monopoly, 2 },
                { DevelopmentCardType.RoadBuilder, 2 },
                { DevelopmentCardType.YearOfPlenty, 2 }
            }
        );

        public static GameType Expansion = new GameType(
            "Expansion", 
            5, 6,
            new List<PlayerColor> {
                PlayerColor.Red, 
                PlayerColor.White, 
                PlayerColor.Blue, 
                PlayerColor.Orange,
                PlayerColor.Green,
                PlayerColor.Brown
            },
            new Dictionary<ResourceType, int>()
            {
                { ResourceType.Brick, 24 },
                { ResourceType.Wood, 24 },
                { ResourceType.Sheep, 24 },
                { ResourceType.Wheat, 24 },
                { ResourceType.Ore, 24 }
            },
            new Dictionary<DevelopmentCardType, int>()
            {
                { DevelopmentCardType.Knight, 20 },
                { DevelopmentCardType.Progress, 5 },
                { DevelopmentCardType.Monopoly, 3 },
                { DevelopmentCardType.RoadBuilder, 3 },
                { DevelopmentCardType.YearOfPlenty, 3 }
            }
        );
    }
}