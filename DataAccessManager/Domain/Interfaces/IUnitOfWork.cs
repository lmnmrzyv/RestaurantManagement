using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IMenuItemRepository MenuItemRepository { get; }
        IOrderRepository OrderRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IDiscountRepository DiscountRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IPositionRepository PositionRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IReservationRepository ReservationRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        ITableRepository TableRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
