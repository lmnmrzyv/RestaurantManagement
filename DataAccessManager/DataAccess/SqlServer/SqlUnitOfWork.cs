using DataAccessManager.Domain.Interfaces;
using DataAccessManager.Domain.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;
        public SqlUnitOfWork(string dataSource, string dbName)
        {
            var builder = new SqlConnectionStringBuilder();

            builder.DataSource = dataSource;
            builder.InitialCatalog = dbName;
            builder.IntegratedSecurity = true;

            _connectionString = builder.ConnectionString;
        }
        public IEmployeeRepository EmployeeRepository => new SqlEmployeeRepository(_connectionString);
        public ICategoryRepository CategoryRepository => new SqlCategoryRepository(_connectionString);
        public IPositionRepository PositionRepository => new SqlPositionRepository(_connectionString);
        public IMenuItemRepository MenuItemRepository => new SqlMenuItemRepository(_connectionString);
        public IDepartmentRepository DepartmentRepository => new SqlDepartmentRepository(_connectionString);
        public IReservationRepository ReservationRepository => new SqlReservationRepository(_connectionString);
        public IOrderDetailRepository OrderDetailRepository => new SqlOrderDetailRepository(_connectionString);
        public ITableRepository TableRepository =>  new SqlTableRepository(_connectionString);
        public IOrderRepository OrderRepository => new SqlOrderRepository(_connectionString);
        public ICustomerRepository CustomerRepository => new SqlCustomerRepository(_connectionString);
        public IDiscountRepository DiscountRepository => new SqlDiscountRepository(_connectionString);
    }
}
