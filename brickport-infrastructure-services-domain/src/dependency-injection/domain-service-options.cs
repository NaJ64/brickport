namespace BrickPort.Infrastructure.Services.Domain.DependencyInjection
{
    public interface IBrickPortDomainServiceOptions 
    {
        bool UseCommands { get; set; }
        bool UseQueries { get; set; }
    }

    public class BrickPortDomainServiceOptions : IBrickPortDomainServiceOptions 
    { 
        public bool UseCommands { get; set; }
        public bool UseQueries { get; set; }
    } 
}