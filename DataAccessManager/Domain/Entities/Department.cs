using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Entities
{
    public class Department:IDbEntity
    {
        public int Id { get; set; }
        public string Name { get; set;}
    }
}
