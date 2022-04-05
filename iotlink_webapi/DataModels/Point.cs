using MongoDB.Bson.Serialization.Attributes;

namespace iotlink_webapi.DataModels
{
    public class Point
    {
        [BsonElement("")]
        public double X { get; set; }
        [BsonElement("")]
        public double Y { get; set; }
    }
}
