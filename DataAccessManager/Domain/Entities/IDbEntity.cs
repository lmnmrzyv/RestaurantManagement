using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Entities
{
    public interface IDbEntity
    {
        int Id { get; set; }
        bool IsActive { get; set; }
    }
}
