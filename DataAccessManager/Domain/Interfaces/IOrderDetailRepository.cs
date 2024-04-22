using System;
using System.Collections.Generic;
using System.Text;
using DataAccessManager.Domain.Entities;

namespace DataAccessManager.Domain.Interfaces
{
    public interface IOrderDetailRepository:ICrudRepository<OrderDetail>
    {
    }
}
