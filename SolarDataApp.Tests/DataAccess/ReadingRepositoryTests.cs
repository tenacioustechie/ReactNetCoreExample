using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SolarDataApp.DataAccess;
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

    public class ReadingRepositoryTests
    {
        [Fact]
        public void CanCreateRepository()
        {
            var sut = new ReadingRepository(TestHelper.GetDataAccessHelper());

            Assert.NotNull(sut);
        }

        [Fact]
        public void CanCreateReading()
        {
            var sut = new ReadingRepository(TestHelper.GetDataAccessHelper());
            var reading = new ReadingModel() {
                Day = DateTime.Today.AddDays(-1),
                LocationId = 1,
                PowerUsed = 19.8M,
                SolarGenerated = 30.1M
            };

            var taskCreate = sut.CreateReading(reading);
            taskCreate.Wait();
            var item = taskCreate.Result;

            Assert.NotNull(item);
            Assert.True(item.Id > 0);
            Assert.Equal(reading.Day, item.Day);
            Assert.Equal(reading.PowerUsed, item.PowerUsed);
            Assert.Equal(reading.SolarGenerated, item.SolarGenerated);
            sut.DeleteReading(item.Id).Wait();
        }

        [Fact]
        public void CanGetReadingsForLocation()
        {
            var sut = new ReadingRepository(TestHelper.GetDataAccessHelper());

            var taskGet = sut.GetReadingsForLocation(1);
            taskGet.Wait();

            Assert.NotNull(taskGet.Result);
            Assert.NotEmpty(taskGet.Result);
        }

        [Fact]
        public void CanGetReading()
        {
            var sut = new ReadingRepository(TestHelper.GetDataAccessHelper());

            var taskGet = sut.GetReading(1);
            taskGet.Wait();

            Assert.NotNull(taskGet.Result);
            Assert.Equal(1, taskGet.Result.Id);
        }

        [Fact]
        public void CantCreateReadingWithNoLocationId()
        {
            var sut = new ReadingRepository(TestHelper.GetDataAccessHelper());
            var reading = new ReadingModel() {
                Day = DateTime.Today.AddDays(-1),
                PowerUsed = 19.8M,
                SolarGenerated = 30.1M
            };

            Assert.ThrowsAsync<ArgumentException>(() => sut.CreateReading(reading)).Wait();
        }

        [Fact]
        public void CanDeleteReading()
        {
            var sut = new ReadingRepository(TestHelper.GetDataAccessHelper());
            var reading = new ReadingModel() {
                Day = DateTime.Today.AddDays(-1),
                LocationId = 1,
                PowerUsed = 19.8M,
                SolarGenerated = 30.1M
            };
            var taskCreate = sut.CreateReading(reading);
            taskCreate.Wait();
            var item = taskCreate.Result;
            Assert.NotNull(item);
            Assert.True(item.Id > 0);

            var taskDelete = sut.DeleteReading(item.Id);
            taskDelete.Wait();

            var taskGet = sut.GetReading(item.Id);
            taskGet.Wait();
            Assert.Null(taskGet.Result);
        }

        [Fact]
        public void CanUpdateReading()
        {
            var sut = new ReadingRepository(TestHelper.GetDataAccessHelper());
            var reading = new ReadingModel() {
                Day = DateTime.Today.AddDays(-1),
                LocationId = 1,
                PowerUsed = 19.8M,
                SolarGenerated = 30.1M
            };
            var taskCreate = sut.CreateReading(reading);
            taskCreate.Wait();
            var item = taskCreate.Result;
            Assert.NotNull(item);
            Assert.True(item.Id > 0);
            var random = new Random();

            item.PowerUsed = random.Next(5, 60);
            item.SolarGenerated = random.Next(50, 90);
            var taskUpdate = sut.UpdateReading(item);
            taskUpdate.Wait();
            var updatedItem = taskUpdate.Result;

            Assert.NotNull(updatedItem);
            Assert.Equal(item.Id, updatedItem.Id);
            Assert.Equal(item.LocationId, updatedItem.LocationId);
            Assert.Equal(item.PowerUsed, updatedItem.PowerUsed);
            Assert.Equal(item.SolarGenerated, updatedItem.SolarGenerated);
        }
    }
}
