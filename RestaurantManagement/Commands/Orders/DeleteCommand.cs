using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Orders
{
    public class DeleteCommand : ICommand
    {
        private readonly IUnitOfWork _db;
        private readonly OrdersViewModel _viewModel;
        public DeleteCommand(IUnitOfWork db, OrdersViewModel viewModel)
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
            var result = MessageBox.Show("Are you sure you want to delete selected item?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            var deletedId = _viewModel.SelectedOrder.Id;

            _db.OrderRepository.Delete(deletedId);

            MessageBox.Show("Successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _viewModel.Orders.Remove(_viewModel.SelectedOrder);
            _viewModel.AllOrders = _viewModel.Orders.ToList();

            for (int i = 0; i < _viewModel.Orders.Count; i++)
            {
                _viewModel.Orders[i].No = i + 1;
            }
            _viewModel.Orders = new ObservableCollection<OrdersModel>(_viewModel.Orders);
            _viewModel.CurrentState = State.NORMAL;
        }
    }
}
