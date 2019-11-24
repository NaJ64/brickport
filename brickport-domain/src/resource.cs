namespace BrickPort.Domain
{
    public class Resource
    {
        public ResourceType ResourceType { get; }
        public Resource(ResourceType resourceType) => ResourceType = resourceType;
    }
}