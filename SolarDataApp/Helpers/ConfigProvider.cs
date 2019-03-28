using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SolarDataApp.Helpers
{
    public interface IConfigProvider
    {
        string ConnectionString { get; set; }
    }

    public class ConfigProvider : IConfigProvider
    {
        public ConfigProvider(IConfiguration configuration)
        {
            ConnectionString = configuration.GetSection("DatabaseConnString").Get<string>();
        }

        public string ConnectionString { get; set; }
    }
}
