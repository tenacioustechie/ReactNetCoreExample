using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SolarDataApp.Helpers;
using SolarDataApp.Models;

namespace SolarDataApp.DataAccess
{
    public interface ILocationRepository
    {
        Task<List<LocationModel>> GetAll();
        Task<LocationModel> GetLocation(int id);
        Task DeleteLocation(int id);
        Task<LocationModel> UpdateLocation(LocationModel location);
        Task<LocationModel> CreateLocation(LocationModel location);
    }

    public class LocationRepository : ILocationRepository
    {
        private IDataAccessHelper _dataAccessHelper;

        public LocationRepository(IDataAccessHelper dataAccessHelper)
        {
            _dataAccessHelper = dataAccessHelper;
        }
        public async Task<List<LocationModel>> GetAll()
        {
            using (var connection = _dataAccessHelper.CreateConnection()) {
                await connection.OpenAsync();
                const string sql = "select * from Locations";
                var list = (await connection.QueryAsync<LocationModel>(sql)).ToList();
                return list;
            }
        }

        public async Task<LocationModel> GetLocation(int id)
        {
            using (var connection = _dataAccessHelper.CreateConnection()) {
                await connection.OpenAsync();
                const string sql = "select * from Locations where Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var location = await connection.QuerySingleOrDefaultAsync<LocationModel>(sql, parameters);
                return location;
            }
        }

        public async Task DeleteLocation(int id)
        {
            using (var connection = _dataAccessHelper.CreateConnection()) {
                await connection.OpenAsync();
                // NOTE deleting out of both parent and child table to avoid error on delete as children still exist
                const string sql = "delete from Readings where LocationId = @Id; delete from Locations where Id = @Id;";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<LocationModel> UpdateLocation(LocationModel location)
        {
            using (var connection = _dataAccessHelper.CreateConnection()) {
                await connection.OpenAsync();
                const string sql = "update Locations set Name = @Name where Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", location.Id);
                parameters.Add("@Name", location.Name);
                await connection.ExecuteAsync(sql, parameters);
                var locationFromDb = await GetLocation(location.Id);
                return locationFromDb;
            }
        }

        public async Task<LocationModel> CreateLocation(LocationModel location)
        {
            using (var connection = _dataAccessHelper.CreateConnection()) {
                await connection.OpenAsync();
                const string sql = "insert into Locations ( Name) values (@Name); Select LAST_INSERT_ID();";
                var parameters = new DynamicParameters();
                parameters.Add("@Name", location.Name);
                var result = await connection.ExecuteScalarAsync(sql, parameters);
                var id = Convert.ToInt32(result);
                var locationFromDb = await GetLocation(id);
                return locationFromDb;
            }
        }
    }
}
