using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace iotlink_webapi.DataModels
{
    public class Geometry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("coordinates")]
        public IList<IList<double>> Coordinates { get; set; }
    }
}
