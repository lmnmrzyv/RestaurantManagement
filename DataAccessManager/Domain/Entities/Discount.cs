using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Entities
{
    public class Discount : IDbEntity
    {
        public int Id { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }
        public int Percent {  get; set; }
    }
}
