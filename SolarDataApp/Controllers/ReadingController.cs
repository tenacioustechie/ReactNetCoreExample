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
    public class ReadingController : ControllerBase
    {
        private List<ReadingModel> _readings;

        public ReadingController()
        {
            _readings = new List<ReadingModel>() { new ReadingModel() { Id = 1, LocationId = 1, Day = DateTime.Now.AddDays(-1), PowerUsed = 19.0M, SolarGenerated = 32.2M}};
        }

        // GET: api/Reading
        [HttpGet]
        public async Task<IEnumerable<ReadingModel>> Get()
        {
            return _readings;
        }

        // GET: api/Reading/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ReadingModel> Get(int id)
        {
            var reading = _readings.SingleOrDefault(n => n.Id == id);
            return reading;
        }

        // POST: api/Reading
        [HttpPost]
        public async Task Post([FromBody] ReadingModel reading)
        {
            _readings.Add(reading);
        }

        // PUT: api/Reading/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ReadingModel readingIn)
        {
            var reading = _readings.SingleOrDefault(n => n.Id == id);
            if (reading == null) {
                return;
            }
            reading.Day = readingIn.Day;
            reading.SolarGenerated = readingIn.SolarGenerated;
            reading.PowerUsed = readingIn.PowerUsed;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var reading = _readings.SingleOrDefault(n => n.Id == id);
            if (reading != null) {
                _readings.Remove(reading);
            }
        }
    }
}
