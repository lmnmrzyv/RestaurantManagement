using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class TableModel : IModel
    {
        public int Id { get; set; }
       public int No {  get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }

    }
}
