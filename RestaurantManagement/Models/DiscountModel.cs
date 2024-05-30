using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class DiscountModel : IModel
    {
        public int Id { get; set; }
        public int No { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public bool Status { get; set; }
        public CategoriesModel Category { get; set; }
        public double Percent { get; set; }
    }
}
