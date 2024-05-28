using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class CategoriesModel : IModel
    {
        public int Id { get; set; }
        public int No {  get; set; }
        public string name { get; set; }
    }
}
