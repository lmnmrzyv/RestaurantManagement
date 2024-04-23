using DataAccessManager.Domain.Interfaces;
using DataAccessManager.Domain.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository => new SqlEmployeeRepository(null);
        public ICategoryRepository CategoryRepository => new SqlCategoryRepository(null);
        public IPositionRepository PositionRepository => new SqlPositionRepository(null);
        public IMenuItemRepository MenuItemRepository => new SqlMenuItemRepository(null);
        public IDepartmentRepository DepartmentRepository => new SqlDepartmentRepository(null);
        public IReservationRepository ReservationRepository => new SqlReservationRepository(null);
        public IOrderDetailRepository OrderDetailRepository => new SqlOrderDetailRepository(null);
        public ITableRepository TableRepository =>  new SqlTableRepository(null);
        public IOrderRepository OrderRepository => new SqlOrderRepository(null);
        public ICustomerRepository CustomerRepository => new SqlCustomerRepository(null);
        public IDiscountRepository DiscountRepository => new SqlDiscountRepository(null);
    }
}
