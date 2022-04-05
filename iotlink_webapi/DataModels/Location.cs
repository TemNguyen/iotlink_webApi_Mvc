using MongoDB.Bson.Serialization.Attributes;

namespace iotlink_webapi.DataModels
{
    public class Location
    {
        [BsonElement("lat")]
        public double Lat { get; set; }
        [BsonElement("lng")]
        public double Lng { get; set; }
    }
}
