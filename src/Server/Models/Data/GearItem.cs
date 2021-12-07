using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#nullable disable

namespace Trailblazor.Server.Models.Data
{
    public record GearItem : BaseDocument
    {
        public Guid OwnerId { get; init; }

        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Link { get; init; } = string.Empty;

        public bool IsFavorite { get; init; } = false;
        public bool IsConsumable { get; init; } = false;
        public bool IsWearable { get; init; } = false;
    }
}
