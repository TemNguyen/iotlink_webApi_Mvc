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
        public async Task<IActionResult> GetPlace()
        {
            var places = await _placeServices.Get();
            return Ok(places);
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<PlaceEntity>> GetPlaceByName([FromRoute ]string name)
        {
            var place = await _placeServices.Get(name);

            if (place == null)
                return NotFound();
            return place;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlaceEntity place)
        {
            await _placeServices.Create(place);
            return CreatedAtAction(nameof(GetPlace), new { name = place.Name }, place);

        }

        [HttpPut]
        public async Task<IActionResult> Update(string name, PlaceEntity placeIn)
        {
            var place = await _placeServices.Get(name);

            if (place == null)
            {
                return NotFound();
            }

            await _placeServices.Update(name, place);
            return Content("Success");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string name)
        {
            var place = await _placeServices.Get(name);

            if (place == null) { return NotFound(); }

            await _placeServices.Remove(place);

            return Content("Success");
        }
    }
}
