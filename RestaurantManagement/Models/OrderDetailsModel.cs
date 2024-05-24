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
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public OrderDetailsModel Clone()
        {
            var orderDetailModel = new OrderDetailsModel();
            orderDetailModel.Id = Id;
            orderDetailModel.No = No;
            orderDetailModel.OrderId = OrderId;
            orderDetailModel.MenuItemId = MenuItemId;
            orderDetailModel.Quantity = Quantity;
            orderDetailModel.Amount = Amount;

            return orderDetailModel;
            
        }
    }
}
