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
    }
}
