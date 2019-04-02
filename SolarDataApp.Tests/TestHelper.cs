using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.Json;
using SolarDataApp.Helpers;

namespace SolarDataApp.Tests
{
    public class TestHelper
    {
        public static IConfiguration GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                //    .AddUserSecrets("e3dfcccf-0cb3-423a-b302-e3e92e95c128")
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
