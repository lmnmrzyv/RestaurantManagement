using DataAccessManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Interfaces
{
    public interface IPositionRepository : ICrudRepository<Position>
    {
    }
}
