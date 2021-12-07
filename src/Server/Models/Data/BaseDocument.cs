using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#nullable disable

namespace Trailblazor.Server.Models.Data
{
    public abstract record BaseDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }

        public DateTimeOffset Created { get; init; }
        public DateTimeOffset LastModified { get; init; }
    }
}
