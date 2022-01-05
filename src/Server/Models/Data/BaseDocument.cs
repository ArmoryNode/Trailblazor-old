using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#nullable disable

namespace Trailblazor.Server.Models.Data
{
    /// <summary>
    ///     The base document for MongoDB entities.
    /// </summary>
    /// <param name="Id">
    ///     The ObjectId of the document.
    /// </param>
    public abstract record BaseDocument([property:BsonId] [property:BsonRepresentation(BsonType.ObjectId)] string Id)
    {
        public DateTimeOffset Created { get; init; } = DateTimeOffset.UtcNow;
        public DateTimeOffset LastModified { get; set; } = DateTimeOffset.UtcNow;
    }
}
