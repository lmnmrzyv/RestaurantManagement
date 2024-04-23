using DataAccessManager.Domain.Interfaces;
using DataAccessManager.Domain.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        public ICategoryRepository CategoryRepository => new SqlCategoryRepository();

        public IMenuItemRepository MenuItemRepository => new SqlMenuItemRepository();

        public IOrderRepository OrderRepository => new SqlOrderRepository();
        public ICustomerRepository CustomerRepository => new SqlCustomerRepository();
        public IDiscountRepository discountRepository => new SqlDiscountRepository();
    }
}
