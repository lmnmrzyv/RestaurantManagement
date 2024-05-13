using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class TablesModel : IModel
    {
        public int No { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
    }
}
