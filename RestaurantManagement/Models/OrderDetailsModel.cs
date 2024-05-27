using DataAccessManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class OrderDetailsModel : IModel
    {
        public int Id { get; set; }
        public int No { get; set; }
        public Order Order { get; set; }
        public MenuItem MenuItem { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public OrderDetailsModel Clone()
        {
            var orderDetailModel = new OrderDetailsModel();
            orderDetailModel.Id = Id;
            orderDetailModel.No = No;
            orderDetailModel.Order = Order;
            orderDetailModel.MenuItem = MenuItem;
            orderDetailModel.Quantity = Quantity;
            orderDetailModel.Amount = Amount;

            return orderDetailModel;
            
        }
    }
}
