using BrickPort.Infrastructure.DependencyInjection;

namespace BrickPort.Web.DependencyInjection
{
    public interface IBrickPortWebOptions : IBrickPortInfrastructureOptions { }

    public class BrickPortWebOptions : BrickPortInfrastructureOptions, IBrickPortWebOptions { }
}