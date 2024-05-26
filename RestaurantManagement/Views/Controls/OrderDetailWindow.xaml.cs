using RestaurantManagement.Models;
using System;
using System.Windows;

namespace RestaurantManagement.Views.Controls
{
    public partial class OrderDetailWindow : Window
    {
        public OrderDetailWindow(OrdersModel order)
        {
            InitializeComponent();
            DataContext = order;
        }

    }
}
