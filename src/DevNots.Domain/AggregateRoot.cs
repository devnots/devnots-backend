using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DevNots.Domain
{
    public abstract class AggregateRoot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; protected set; }
    }
}
