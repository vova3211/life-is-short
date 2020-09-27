using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LifeIsShort.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class BaseEntity
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
