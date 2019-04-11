using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SolarDataApp.Helpers;
using SolarDataApp.Models;

namespace SolarDataApp.DataAccess
{
    public interface IReadingRepository
    {
        Task<ReadingModel> GetReading(int id);
        Task<List<ReadingModel>> GetReadingsForLocation(int locationId);
        Task DeleteReading(int id);
        Task<ReadingModel> UpdateReading(ReadingModel reading);
        Task<ReadingModel> CreateReading(ReadingModel reading);
    }
    public class ReadingRepository : IReadingRepository
    {
        private IDataAccessHelper _dataAccessHelper;

        public ReadingRepository(IDataAccessHelper dataAccessHelper)
        {
            _dataAccessHelper = dataAccessHelper;
        }

        public async Task<ReadingModel> CreateReading(ReadingModel reading)
        {
            if (reading.LocationId <= 0) {
                throw new ArgumentException("Reading's must have a Location associated with them", "LocationId");
            }
            using (var connection = _dataAccessHelper.CreateConnection()) {
                await connection.OpenAsync();
                const string sql = "insert into Readings ( LocationId, Day, SolarGenerated, PowerUsed) values (@LocationId, @Day, @SolarGenerated, @PowerUsed); Select LAST_INSERT_ID();";
                var parameters = new DynamicParameters();
                parameters.Add("@LocationId", reading.LocationId);
                parameters.Add("@Day", reading.Day);
                parameters.Add("@SolarGenerated", reading.SolarGenerated);
                parameters.Add("@PowerUsed", reading.PowerUsed);
                var result = await connection.ExecuteScalarAsync(sql, parameters);
                var id = Convert.ToInt32(result);
                var readingFromDb = await GetReading(id);
                return readingFromDb;
            }
        }
        public async Task<ReadingModel> GetReading(int id)
        {
            using (var connection = _dataAccessHelper.CreateConnection()) {
                await connection.OpenAsync();
                const string sql = "select * from Readings where Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var reading = await connection.QuerySingleOrDefaultAsync<ReadingModel>(sql, parameters);
                return reading;
            }
        }

        public async Task DeleteReading(int id)
        {
            using (var connection = _dataAccessHelper.CreateConnection()) {
                await connection.OpenAsync();
                // NOTE deleting out of both parent and child table to avoid error on delete as children still exist
                const string sql = "delete from Readings where Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<List<ReadingModel>> GetReadingsForLocation(int locationId)
        {
            using (var connection = _dataAccessHelper.CreateConnection()) {
                await connection.OpenAsync();
                const string sql = "select * from Readings where LocationId = @LocationId";
                var parameters = new DynamicParameters();
                parameters.Add("@LocationId", locationId);
                var reading = (await connection.QueryAsync<ReadingModel>(sql, parameters)).ToList();
                return reading;
            }
        }

        public async Task<ReadingModel> UpdateReading(ReadingModel reading)
        {
            using (var connection = _dataAccessHelper.CreateConnection()) {
                await connection.OpenAsync();
                const string sql = "update Readings set Day = @Day, SolarGenerated = @SolarGenerated, PowerUsed = @PowerUsed where Id = @Id; select * from Readings where Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", reading.Id);
                parameters.Add("@Day", reading.Day);
                parameters.Add("@PowerUsed", reading.PowerUsed);
                parameters.Add("@SolarGenerated", reading.SolarGenerated);
                var readingFromDb = await connection.QuerySingleOrDefaultAsync<ReadingModel>(sql, parameters);
                return readingFromDb;
            }
        }
    }
}
