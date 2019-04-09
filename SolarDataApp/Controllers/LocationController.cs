using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarDataApp.DataAccess;
using SolarDataApp.Models;

namespace SolarDataApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController( ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        // GET: api/Location
        [HttpGet]
        public async Task<IEnumerable<LocationModel>> Get()
        {
            return await _locationRepository.GetAll();
        }

        // GET: api/Location/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<LocationModel> Get(int id)
        {
            var location = await _locationRepository.GetLocation(id);
            return location;
        }

        // POST: api/Location
        [HttpPost]
        public async Task<LocationModel> Post([FromBody] LocationModel locationIn)
        {
            var location = locationIn;
            if (locationIn.Id > 0) {
                // update the existing location
                location = await _locationRepository.GetLocation(locationIn.Id);
                if (location == null) {
                    Response.StatusCode = 404;
                    return null;
                }
                location.Name = locationIn.Name;
                return await _locationRepository.UpdateLocation(location);
            }
            else {
                // create a new location
                return await _locationRepository.CreateLocation(location);
            }
        }

        // PUT: api/Location/5
        [HttpPut("{id}")]
        public async Task<LocationModel> Put(int id, [FromBody] LocationModel locationIn)
        {
            var location = await _locationRepository.GetLocation(id);
            if (location == null) {
                Response.StatusCode = 404;
                return null;
            }
            location.Name = locationIn.Name;
            var locationFromDb = await _locationRepository.UpdateLocation(location);
            return locationFromDb;

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var location = await _locationRepository.GetLocation(id);
            if (location != null) {
                await _locationRepository.DeleteLocation(id);
            } else {
                Response.StatusCode = 404;
            }
        }
    }
}
