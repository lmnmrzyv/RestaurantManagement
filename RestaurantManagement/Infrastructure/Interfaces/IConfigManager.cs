using RestaurantManagement.Infrastructure.Models;
using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Interfaces
{
    public interface IConfigManager
    {
        DatabaseConfigModel GetDatabaseConfig();
    }
}
