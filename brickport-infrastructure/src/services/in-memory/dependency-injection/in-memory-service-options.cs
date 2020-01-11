namespace BrickPort.Infrastructure.Services.InMemory.DependencyInjection
{
    public interface IBrickPortInMemoryServiceOptions 
    {
        bool UseCommands { get; set; }
        bool UseQueries { get; set; }
    }

    public class BrickPortInMemoryServiceOptions : IBrickPortInMemoryServiceOptions 
    { 
        public bool UseCommands { get; set; }
        public bool UseQueries { get; set; }
    }
        
}