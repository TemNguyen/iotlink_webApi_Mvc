using iotlink_webapi.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iotlink_webapi.Services
{
    public interface IPlaceService
    {
        public Task<List<PlaceEntity>> Get();
        public Task<PlaceEntity> Get(string id);
        public Task<PlaceEntity> Create(PlaceEntity place);
        public Task Update(string id, PlaceEntity place);
        public Task Remove(PlaceEntity placeIn);

    }
}
