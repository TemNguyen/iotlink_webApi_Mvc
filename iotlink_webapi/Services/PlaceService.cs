using iotlink_webapi.DataModels;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<List<PlaceEntity>> Get() => 
            await _places.Find(place => true).ToListAsync();
        public async Task<PlaceEntity> Get(string name) =>
            await _places.Find<PlaceEntity>(place => place.Name == name).FirstOrDefaultAsync();

        public async Task<PlaceEntity> Create(PlaceEntity place)
        {
            await _places.InsertOneAsync(place);
            return place;
        }

        public async Task Update(string name, PlaceEntity placeIn)
        {
           await _places.ReplaceOneAsync(place => place.Name == name, placeIn);
        }

        public async Task Remove(PlaceEntity placeIn)
        {
            await _places.DeleteOneAsync(place => place.Name == placeIn.Name);
        }

    }
}
