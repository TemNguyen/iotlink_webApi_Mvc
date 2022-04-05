using iotlink_webapi.DataModels;
using MongoDB.Driver;
using System.Collections.Generic;

namespace iotlink_webapi.Services
{
    public class PlaceService
    {
        private readonly IMongoCollection<PlaceEntity> _places;

        public PlaceService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _places = database.GetCollection<PlaceEntity>(settings.CollectionName);

        }

        public List<PlaceEntity> Get() => 
            _places.Find(place => true).ToList();
        public PlaceEntity Get(string name) =>
            _places.Find<PlaceEntity>(place => place.Name == name).FirstOrDefault();

        public PlaceEntity Create(PlaceEntity place)
        {
            _places.InsertOne(place);
            return place;
        }

        public void Update(string name, PlaceEntity placeIn)
        {
            _places.ReplaceOne(place => place.Name == name, placeIn);
        }

        public void Remove(PlaceEntity placeIn)
        {
            _places.DeleteOne(place => place.Name == placeIn.Name);
        }

    }
}
