using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Entities
{
    public class OrderDetail: IDbEntity
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public MenuItem MenuItem { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }
}
