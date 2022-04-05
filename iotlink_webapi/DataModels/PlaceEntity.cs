using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace iotlink_webapi.DataModels
{
    public class PlaceEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("address")]
        public string Address { get; set; }
        [BsonElement("location")]
        public Location Location { get; set; }
        [BsonElement("geometry")]
        public Geometry Geometry { get; set; }
    }
}
