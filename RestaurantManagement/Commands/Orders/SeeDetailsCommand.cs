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
        private readonly IUnitOfWork _db;

        public SeeDetailsCommand(IUnitOfWork db, OrdersViewModel viewModel)
        {
            _db = db;
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _viewModel.SelectedOrders != null;
        }

        public void Execute(object parameter)
        {
            var selectedOrder = _viewModel.SelectedOrders;
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
