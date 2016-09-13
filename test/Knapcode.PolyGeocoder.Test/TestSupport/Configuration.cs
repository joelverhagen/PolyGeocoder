using System.IO;
using Microsoft.Extensions.Configuration;

namespace Knapcode.PolyGeocoder.Test.TestSupport
{
    public static class Configuration
    {
        private static readonly IConfiguration _configuration;

        static Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json", optional: true);

            _configuration = builder.Build();
        }

        public static string BingMapsApiKey => _configuration["BingMapsApiKey"];

        public static string GoogleApiKey => _configuration["GoogleApiKey"];

        public static string MapQuestApiKey => _configuration["MapQuestApiKey"];
    }
}
