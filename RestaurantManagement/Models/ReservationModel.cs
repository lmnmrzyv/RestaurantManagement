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
        public  TableModel Table { get; set; }
        public CustomerModel Customer { get; set; }
    }
}
