using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Entities
{
    public class Customer : IDbEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNum { get; set; }
        public string Mail { get; set; }
    }
}
