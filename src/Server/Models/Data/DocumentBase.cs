using System.Text.Json.Serialization;

namespace Trailblazor.Server.Models.Data
{
    public abstract record DocumentBase([property:JsonPropertyName("id")] Guid Id)
    {
        public DateTimeOffset Created { get; init; } = DateTimeOffset.UtcNow;
        public DateTimeOffset LastModified { get; set; } = DateTimeOffset.UtcNow;
    }
}
