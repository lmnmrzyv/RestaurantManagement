using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Models;

namespace RestaurantManagement.ViewModels
{
    public class TablesViewModel
    {
        public TablesViewModel()
        {

        }
        public List<TablesModel> Tables { get; set; }
    }
}
