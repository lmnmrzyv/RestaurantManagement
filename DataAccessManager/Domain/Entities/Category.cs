using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Entities
{
    public class Category : IDbEntity
    {
        public int Id { get; set; }
        public string name { get; set; }
        public bool IsActive { get; set; }

    }
}
