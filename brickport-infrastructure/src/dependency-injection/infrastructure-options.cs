namespace BrickPort.Infrastructure.DependencyInjection
{
    public interface IBrickPortInfrastructureOptions
    {
        bool UseDomainCommands { get; set; }
        bool UseDomainQueries { get; set; }
        bool UseInMemoryCommands { get; set; }
        bool UseInMemoryQueries { get; set; }
    }

    public class BrickPortInfrastructureOptions : IBrickPortInfrastructureOptions
    {
        public bool UseDomainCommands { get; set; }
        public bool UseDomainQueries { get; set; }
        public bool UseInMemoryCommands { get; set; }
        public bool UseInMemoryQueries { get; set; }
        public BrickPortInfrastructureOptions()
        {
            UseDomainCommands = false;
            UseDomainQueries = false;
            UseInMemoryCommands = false;
            UseInMemoryQueries = false;
        }
    }
}