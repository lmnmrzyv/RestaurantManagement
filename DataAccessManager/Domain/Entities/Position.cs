using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Entities
{
    public class Position:IDbEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
        public bool IsActive { get; set; }
    }
}
