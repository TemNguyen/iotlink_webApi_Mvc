using iotlink_webapi.DataModels;
using iotlink_webapi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iotlink_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly PlaceService _placeServices;

        public HomeController(PlaceService placeServices)
        {
            this._placeServices = placeServices;
        }
        [HttpGet]
        public IActionResult GetPlace()
        {
            var places =  _placeServices.Get();
            return Ok(places);
        }

        [HttpGet]
        [Route("{name}")]
        public ActionResult<PlaceEntity> GetPlaceByName([FromRoute ]string name)
        {
            var place = _placeServices.Get(name);

            if (place == null)
                return NotFound();
            return place;
        }

        [HttpPost]
        public IActionResult Create(PlaceEntity place)
        {
            _placeServices.Create(place);
            return CreatedAtAction(nameof(GetPlace), new { name = place.Name }, place);

        }

        [HttpPut]
        public IActionResult Update(string name, PlaceEntity placeIn)
        {
            var place = _placeServices.Get(name);

            if (place == null)
            {
                return NotFound();
            }

            _placeServices.Update(name, place);
            return Content("Success");
        }

        [HttpDelete]
        public IActionResult Delete(string name)
        {
            var place = _placeServices.Get(name);

            if (place == null) { return NotFound(); }

            _placeServices.Remove(place);

            return Content("Success");
        }
    }
}
