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
    public class ReadingController : ControllerBase
    {
        private IReadingRepository _readingsRepository;

        public ReadingController( IReadingRepository readingsRepository)
        {
            _readingsRepository = readingsRepository;
        }

        // GET: api/Reading
        [HttpGet("{locationId}")]
        public async Task<IEnumerable<ReadingModel>> Get( int locationId)
        {
            return await _readingsRepository.GetReadingsForLocation( locationId);
        }

        // POST: api/Reading
        [HttpPost]
        public async Task<ReadingModel> Post([FromBody] ReadingModel reading)
        {
            var readingFromDb = await _readingsRepository.CreateReading(reading);
            return readingFromDb;
        }

        // PUT: api/Reading/5
        [HttpPut("{id}")]
        public async Task<ReadingModel> Put(int id, [FromBody] ReadingModel readingIn)
        {
            var readingFromDb = await _readingsRepository.GetReading(readingIn.Id);
            if (readingFromDb == null) {
                Response.StatusCode = 404;
                return null;
            }

            readingFromDb.Day = readingIn.Day;
            readingFromDb.PowerUsed = readingIn.PowerUsed;
            readingFromDb.SolarGenerated = readingIn.SolarGenerated;
            var readingResult = await _readingsRepository.UpdateReading(readingFromDb);
            return readingResult;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var readingFromDb = await _readingsRepository.GetReading(id);
            if (readingFromDb == null) {
                Response.StatusCode = 404;
                return;
            }

            await _readingsRepository.DeleteReading(id);
        }
    }
}
