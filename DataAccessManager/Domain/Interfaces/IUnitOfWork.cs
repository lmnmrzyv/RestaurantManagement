using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        IPositionRepository PositionRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IReservationRepository ReservationRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        ITableRepository TableRepository { get; }
    }
}
