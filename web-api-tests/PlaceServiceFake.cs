using iotlink_webapi.DataModels;
using iotlink_webapi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_api_tests
{
    internal class PlaceServiceFake : IPlaceService
    {
        private readonly List<PlaceEntity> _places;

        public PlaceServiceFake()
        {
            _places = new List<PlaceEntity>()
            {
                new PlaceEntity() { Id = "624d999263753de135aa82e3", Name = "test1", Address = "Dana",
                    Location = new Location() {
                        Lat = 10, Lng = 20
                    },
                    Geometry = new Geometry() {
                        Type = "Point",
                        Coordinates =
                        {
                            new List<double>() {
                                108.8525390625, 14.322937322075674
                            }
                        }
                    }
                },
                new PlaceEntity() { Id = "624d9a46db2c3f6eff38f677", Name = "test2", Address = "Dana",
                    Location = new Location() {
                        Lat = 10, Lng = 2
                    },
                    Geometry = new Geometry() {
                        Type = "Polygon",
                        Coordinates =
                        {
                            new List<double>() {
                                93.1640625, 59.5343180010956
                            },
                            new List<double>() {
                                109.3359375, 59.5343180010956
                            },
                            new List<double>() {
                                108.8525390625, 14.322937322075674
                            },
                            new List<double>() {
                                108.8525390625, 14.322937322075674
                            },
                            new List<double>() {
                                108.8525390625, 14.322937322075674
                            },
                        }
                    }
                }
            };
        }
        public Task<PlaceEntity> Create(PlaceEntity place)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlaceEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<PlaceEntity> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(PlaceEntity placeIn)
        {
            throw new NotImplementedException();
        }

        public Task Update(string id, PlaceEntity place)
        {
            throw new NotImplementedException();
        }
    }
}
