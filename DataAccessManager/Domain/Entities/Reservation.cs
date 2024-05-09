using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Entities
{
    public class Reservation:IDbEntity
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfPeople { get; set; }
        public Table Table { get; set; }
        public Customer Customer { get; set; }
        public bool IsActive { get; set; }
    }
}
