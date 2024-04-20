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
        public int TableId { get; set; }
        public int CustomerId { get; set; }

    }
}
