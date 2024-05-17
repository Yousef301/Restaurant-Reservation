using Microsoft.Extensions.Configuration;
using System.IO;

namespace RestaurantReservation.Helpers
{
    public class AppSettingsManager
    {
        private static readonly IConfiguration _configuration;

        static AppSettingsManager()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            while (!Directory.Exists(Path.Combine(currentDirectory, "RestaurantReservation")))
            {
                currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
                if (currentDirectory == null)
                {
                    throw new DirectoryNotFoundException("Could not find the project directory.");
                }
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("RestaurantReservation/appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
        }

        public static string? GetConnectionString(string section, string key)
        {
            var connectionStrings = _configuration.GetSection(section).GetSection(key).Value;

            return connectionStrings;
        }
    }
}