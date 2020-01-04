using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrickPort.Services.Queries
{
    public class Player 
    {
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
    }
    public interface IPlayerQueries
    {
        Task<IEnumerable<Player>> GetAsync();
        Task<Player> GetAsync(string id);
    }
}
