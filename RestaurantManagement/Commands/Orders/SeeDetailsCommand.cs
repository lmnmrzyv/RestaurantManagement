using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using RestaurantManagement.Views;
using RestaurantManagement.Views.Controls;
using System;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Orders
{
    public class SeeDetailsCommand : ICommand
    {
        private readonly OrdersViewModel _viewModel;

        public SeeDetailsCommand(OrdersViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _viewModel.SelectedOrder != null;
        }

        public void Execute(object parameter)
        {
            var selectedOrder = _viewModel.SelectedOrder;
            if (selectedOrder != null)
            {
                var orderDetailWindow = new OrderDetailWindow(selectedOrder)
                {
                    DataContext = selectedOrder
                };
                orderDetailWindow.Show();
            }
        }
    }
}
