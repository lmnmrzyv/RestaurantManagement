using DataAccessManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class ReservationModel : IModel
    {
        public int Id { get; set; } 
        public int No {  get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfPeople {  get; set; }
        public  Table Table { get; set; }
        public Customer Customer { get; set; }
    }
}
