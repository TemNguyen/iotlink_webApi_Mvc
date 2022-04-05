﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace iotlink_webapi.DataModels
{
    public class Account
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("username")]
        public string Username { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
    }
}
