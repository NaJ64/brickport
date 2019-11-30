namespace BrickPort.Domain.Models
{
    public class Resource
    {
        public ResourceType ResourceType { get; }
        public Resource(ResourceType resourceType) => ResourceType = resourceType;
    }
}