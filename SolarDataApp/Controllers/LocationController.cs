using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarDataApp.Models;

namespace SolarDataApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private List<LocationModel> _locations;

        public LocationController()
        {
            _locations = new List<LocationModel>() {
                new LocationModel() { Id = 1, Name = "Wilga" },
                new LocationModel() { Id = 2, Name = "Dent" }
            };
        }

        // GET: api/Location
        [HttpGet]
        public IEnumerable<LocationModel> Get()
        {
            return _locations;
        }

        // GET: api/Location/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<LocationModel> Get(int id)
        {
            var location = _locations.FirstOrDefault(n => n.Id == id);
            return location;
        }

        // POST: api/Location
        [HttpPost]
        public async Task Post([FromBody] LocationModel location)
        {
            _locations.Add(location);
        }

        // PUT: api/Location/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] LocationModel locationIn)
        {
            var location = _locations.FirstOrDefault(n => n.Id == id);
            if (location == null) {
                return;
            }
            location.Name = locationIn.Name;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var location = _locations.FirstOrDefault(n => n.Id == id);
            if (location != null) {
                _locations.Remove(location);
            }
        }
    }
}
