using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using SolarDataApp.Helpers;

namespace SolarDataApp.Tests
{
    public class TestHelper
    {
        public static IConfiguration GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets("24fa0558-e29e-49af-b4f7-3e1687c405f8")
                //    .AddEnvironmentVariables()
                .Build();
        }

        public static IConfigProvider GetRealConfigProvider()
        {
            return new ConfigProvider(GetIConfigurationRoot());
        }

        public static IDataAccessHelper GetDataAccessHelper()
        {
            return new DataAccessHelper(GetRealConfigProvider());
        }
    }
}
