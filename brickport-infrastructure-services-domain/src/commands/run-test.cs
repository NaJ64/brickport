using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BrickPort.Domain.Models;
using BrickPort.Domain.Models.PlayerActions;
using BrickPort.Services.Commands;

namespace BrickPort.Infrastructure.Services.Domain.Commands
{
    public class DomainRunTestHandler : IRunTestHandler 
    { 
        public Task<string> HandleAsync(RunTestCommand command)
        {
            var playerNate = new Player("Nate");
            var playerDaniel = new Player("Daniel");
            var playerLuke = new Player("Luke");

            var players = new Dictionary<PlayerColor, Player>();
            players[PlayerColor.White] = playerNate;
            players[PlayerColor.Blue] = playerDaniel;
            players[PlayerColor.Red] = playerLuke;
            
            PlayerColor playerFor(string playerName)
            {
                return players.SingleOrDefault(x => x.Value.Name == playerName).Key;
            };

            var game = new Game(GameType.Standard, players);

            game.AddTurn(new PreGamePlayerTurn(playerFor("Nate"), new List<IPreGameAction>()
            {
                new BuildSettlement(playerFor("Nate"), (
                    new Tile(5, ResourceType.Wheat),
                    new Tile(6, ResourceType.Sheep),
                    new Tile(9, ResourceType.Ore)
                ))
            }));
            
            game.AddTurn(new PreGamePlayerTurn(playerFor("Daniel"), new List<IPreGameAction>()
            {
                new BuildSettlement(playerFor("Daniel"), (
                    new Tile(4, ResourceType.Brick),
                    new Tile(5, ResourceType.Wheat),
                    new Tile(8, ResourceType.Wood)
                ))
            }));
            
            game.AddTurn(new PreGamePlayerTurn(playerFor("Luke"), new List<IPreGameAction>()
            {
                new BuildSettlement(playerFor("Luke"), (
                    new Tile(10, ResourceType.Wheat),
                    new Tile(5, ResourceType.Ore),
                    new Tile(8, ResourceType.Wood)
                ))
            }));
            
            game.AddTurn(new PreGamePlayerTurn(playerFor("Luke"), new List<IPreGameAction>()
            {
                new BuildSettlement(playerFor("Luke"), (
                    new Tile(4, ResourceType.Brick),
                    new Tile(6, ResourceType.Ore),
                    new Tile(10, ResourceType.Wood)
                ))
            }));

            game.AddTurn(new PreGamePlayerTurn(playerFor("Daniel"), new List<IPreGameAction>()
            {
                new BuildSettlement(playerFor("Daniel"), (
                    new Tile(4, ResourceType.Sheep),
                    new Tile(6, ResourceType.Wood),
                    new Tile(3, ResourceType.Brick)
                ))
            }));

            game.AddTurn(new PreGamePlayerTurn(playerFor("Nate"), new List<IPreGameAction>()
            {
                new BuildSettlement(playerFor("Nate"), (
                    new Tile(8, ResourceType.Brick),
                    new Tile(10, ResourceType.Wood),
                    new Tile(9, ResourceType.Ore)
                ))
            }));
            
            var gameState = JsonSerializer.Serialize(game.GetState());

            return Task.FromResult(gameState);
        }
    } 
}