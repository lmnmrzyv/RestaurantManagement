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
        public Category Category { get; set; }
        public double Percent {  get; set; }
        public bool IsActive { get ; set ; }
    }
}
