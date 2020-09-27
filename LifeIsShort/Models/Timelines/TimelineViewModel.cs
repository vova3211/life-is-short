using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LifeIsShort.Models.Timelines
{
    [BsonIgnoreExtraElements]
    public class TimelineViewModel
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public int MaxYears { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
