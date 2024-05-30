using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Orders
{
    public class SaveCommand : ICommand
    {
        private readonly OrdersViewModel _viewModel;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, OrdersViewModel viewModel)
        {
            _db = db;
            _viewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var mapper = new OrderMapper();
            var order = mapper.MapModelToEntity(new Order(), _viewModel.CurrentOrder);
            if (order.Id == 0)
            {
                _viewModel.CurrentOrder.Id = _db.OrderRepository.Add(order);
                var lastElementNo = _viewModel.Orders.LastOrDefault()?.No ?? 0;
                _viewModel.CurrentOrder.No = lastElementNo + 1;
                _viewModel.Orders.Add(_viewModel.CurrentOrder);
                _viewModel.AllOrders = _viewModel.Orders.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var existingOrder = _db.OrderRepository.Get(order.Id);
                _db.OrderRepository.Update(order);
                var index = _viewModel.Orders.IndexOf(_viewModel.Orders.First(x => x.Id == order.Id));
                _viewModel.Orders[index] = _viewModel.CurrentOrder;
                _viewModel.AllOrders = _viewModel.Orders.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            _viewModel.CurrentOrder = new OrdersModel();
            _viewModel.SelectedOrder = null;
            _viewModel.CurrentState = State.NORMAL;
        }

    }
}
