using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class DiscountsModel : IModel
    {
        public int No { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }
        public int Percent { get; set; }
    }
}
