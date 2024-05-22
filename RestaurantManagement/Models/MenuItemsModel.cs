using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class MenuItemsModel : IModel
    {
        public int No { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float price { get; set; }
    }
}
