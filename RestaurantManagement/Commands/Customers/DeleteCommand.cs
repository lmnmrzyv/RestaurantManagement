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

namespace RestaurantManagement.Commands.Customers
{
    public class DeleteCommand : ICommand
    {
        
        private readonly IUnitOfWork _db;
        private readonly CustomersViewModel _viewModel;

        public DeleteCommand(IUnitOfWork db,CustomersViewModel viewModel)
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

            if (result == MessageBoxResult.No)
                return;

            var deletedId = _viewModel.SelectedCustomer.Id;

            _db.CustomerRepository.Delete(deletedId);

            MessageBox.Show("Successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _viewModel.Customers.Remove(_viewModel.SelectedCustomer);
            _viewModel.AllCustomers = _viewModel.Customers.ToList();

            for (int i = 0; i < _viewModel.Customers.Count; i++)
            {
                _viewModel.Customers[i].No = i + 1;
            }
            _viewModel.Customers = new ObservableCollection<CustomerModel>(_viewModel.Customers);
            _viewModel.CurrentState = State.NORMAL;
        }
    }
}
