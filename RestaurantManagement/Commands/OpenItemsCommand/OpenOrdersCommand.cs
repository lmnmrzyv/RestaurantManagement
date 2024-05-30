using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using RestaurantManagement.Views.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RestaurantManagement.Commands
{
    public class OpenOrderCommand : ICommand
    {
        private readonly IUnitOfWork _db;
        public OpenOrderCommand(IUnitOfWork db)
        {
            _db = db;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var grid = parameter as Grid;

            if (grid == null)
                return;

            grid.Children.Clear();

            var control = new OrdersControl();
            var viewModel = new OrdersViewModel(_db);

            var orders = _db.OrderRepository.GetAll();

            var ordersModel = new List<OrdersModel>();

            var ordersMapper = new OrderMapper();
            var no = 1;

            foreach (var order in orders)
            {
                var orderModel = ordersMapper.MapEntityToModel(order, new OrdersModel());

                orderModel.No = no++;

                ordersModel.Add(orderModel);
            }

            viewModel.AllOrders = ordersModel;
            viewModel.Orders = new ObservableCollection<OrdersModel>(ordersModel);

            control.DataContext = viewModel;

            grid.Children.Add(control);
        }
    }
}
