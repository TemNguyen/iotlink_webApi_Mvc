using iotlink_webapi.DataModels;
using iotlink_webapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iotlink_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : ControllerBase
    {
        private readonly PlaceService _placeServices;

        public PlacesController(PlaceService placeServices)
        {
            this._placeServices = placeServices;
        }
        // GET: api/Places
        [HttpGet]
        public async Task<IActionResult> GetPlaces()
        {
            var places = await _placeServices.Get();
            return Ok(places);
        }
        // GET: api/Places/id
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PlaceEntity>> GetPlace([FromRoute]string id)
        {
            var place = await _placeServices.Get(id);

            if (place == null)
                return NotFound();
            return place;
        }
        // POST: api/Places
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PlaceEntity place)
        {
            await _placeServices.Create(place);
            return CreatedAtAction(nameof(GetPlaces), new { name = place.Name }, place);

        }
        // PUT: api/Places/id
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, PlaceEntity place)
        {
            var placeIn = await _placeServices.Get(id);

            if (placeIn == null)
            {
                return NotFound();
            }

            await _placeServices.Update(id, place);
            return Content("Success");
        }
        // DELETE: api/Places/id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var place = await _placeServices.Get(id);

            if (place == null) { return NotFound(); }

            await _placeServices.Remove(place);

            return Content("Success");
        }
    }
}
