using RestaurantManagement.Models;
using RestaurantManagement.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.ViewModels.Implementations
{
    public class FileConfigManager : IConfigManager
    {
        public DatabaseConfigModel GetDatabaseConfig()
        {
            var appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var configFile = Path.Combine(appDataFolderPath, "Restaurant Management", "config.txt");

            if (File.Exists(configFile) == false)
                throw new FileNotFoundException("Config file not found.");

            var lines = File.ReadAllLines(configFile);

            var databaseAddress = lines[0];
            var databaseName = lines[1];

            return new DatabaseConfigModel()
            {
                DBAddress = databaseAddress,
                DBName = databaseName,
            };
        }
    }
}
