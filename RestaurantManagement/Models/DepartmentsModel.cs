using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class DepartmentsModel:IModel
    {
        public int Id { get; set; } 
        public int No { get; set; }
        public string Name { get; set; }
    }
}
