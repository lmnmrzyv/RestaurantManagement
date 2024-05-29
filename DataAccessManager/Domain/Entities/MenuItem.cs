using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Entities
{
    public class MenuItem : IDbEntity
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public Category Category { get; set; }
        public bool IsActive { get; set; }

    }
}
