namespace Trailblazor.Server.Infrastructure
{
    public record CosmosDbSettings
    {
        public string EndpointUrl { get; init; } = string.Empty;
        public string AuthorizationKey { get; init; } = string.Empty;
        public string DatabaseName { get; init; } = string.Empty;
        public string ConnectionString { get; init; } = string.Empty;
    }
}
