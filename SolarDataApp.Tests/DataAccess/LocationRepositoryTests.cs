using SolarDataApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolarDataApp.Models;
using Xunit;

namespace SolarDataApp.Tests.DataAccess
{
    /// <summary>
    /// NOTE: these tests are a little to interdepenedent, they should be more
    /// isolated than they are, but given they are actually testing against a real
    /// database, they will achieve the purpose of checking all individual
    /// calls work. 
    /// </summary>
    
    public class LocationRepositoryTests
    {
        [Fact]
        public void CanCreate()
        {
            var sut = new LocationRepository(TestHelper.GetDataAccessHelper());

            Assert.NotNull(sut);
        }

        [Fact]
        public void CanGetAll()
        {
            var sut = new LocationRepository(TestHelper.GetDataAccessHelper());

            var task = sut.GetAll();
            task.Wait();
            var list = task.Result;

            Assert.NotNull(list);
            Assert.NotEmpty(list);
        }

        [Fact]
        public void CanGetLocation()
        {
            var sut = new LocationRepository(TestHelper.GetDataAccessHelper());

            var task = sut.GetLocation(1);
            task.Wait();
            var item = task.Result;

            Assert.NotNull(item);
        }

        [Fact]
        public void CanUpdateLocation()
        {
            var sut = new LocationRepository(TestHelper.GetDataAccessHelper());
            var location = new LocationModel() {
                Name = "UnitTest Location UpdateMe"
            };
            var taskCreate = sut.CreateLocation(location);
            taskCreate.Wait();
            var item = taskCreate.Result;
            Assert.NotNull(item);

            var random = new Random();
            var newName = "Test Location " + random.Next(10, 99);
            item.Name = newName;
            var taskUpdate = sut.UpdateLocation(item);
            taskUpdate.Wait();
            var itemUpdate = taskUpdate.Result;

            Assert.Equal( newName, itemUpdate.Name);
        }

        [Fact]
        public void CanCreateLocation()
        {
            var sut = new LocationRepository(TestHelper.GetDataAccessHelper());
            var location = new LocationModel() {
                Name = "UnitTest Location"
            };

            var taskCreate = sut.CreateLocation(location);
            taskCreate.Wait();
            var createdLocation = taskCreate.Result;

            Assert.NotNull(createdLocation);
            Assert.Equal(location.Name, createdLocation.Name);
            Assert.True( createdLocation.Id > 0);
            // cleanup
            var taskDelete = sut.DeleteLocation(createdLocation.Id);
            taskDelete.Wait();
        }

        [Fact]
        public void CanDeleteLocation()
        {
            var sut = new LocationRepository(TestHelper.GetDataAccessHelper());
            var location = new LocationModel() {
                Name = "UnitTest Location DeleteMe"
            };
            var taskCreate = sut.CreateLocation(location);
            taskCreate.Wait();
            var createdLocation = taskCreate.Result;
            Assert.NotNull(createdLocation);
            Assert.Equal(location.Name, createdLocation.Name);
            Assert.True(createdLocation.Id > 0);

            var taskDelete = sut.DeleteLocation(createdLocation.Id);
            taskDelete.Wait();

            var taskGet = sut.GetAll();
            taskGet.Wait();
            var list = taskGet.Result;
            var item = list.SingleOrDefault(i => i.Id == createdLocation.Id);
            Assert.Null(item);
        }
    }
}
