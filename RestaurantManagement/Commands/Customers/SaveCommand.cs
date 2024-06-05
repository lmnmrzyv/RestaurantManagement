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

namespace RestaurantManagement.Commands.Customers
{
    public class SaveCommand : ICommand
    {
        private readonly CustomersViewModel _viewModel;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, CustomersViewModel currentState)
        {
            _db = db;
            _viewModel = currentState;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var mapper = new CustomerMapper();
            var customer = mapper.MapModelToEntity(new Customer(), _viewModel.CurrentCustomer);
            customer.IsActive = true;
            if(customer.Id==0)
            {
                _viewModel.CurrentCustomer.Id = _db.CustomerRepository.Add(customer);

                var lastElementNo = _viewModel.AllCustomers.LastOrDefault()?.No ?? 0;

                _viewModel.CurrentCustomer.No = lastElementNo + 1;

                _viewModel.Customers.Add(_viewModel.CurrentCustomer);
                _viewModel.AllCustomers = _viewModel.Customers.ToList();
                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var existingCustomer = _db.CustomerRepository.Get(customer.Id);
                _db.CustomerRepository.Update(customer);
                var index = _viewModel.Customers.IndexOf(_viewModel.Customers.First(x => x.Id == customer.Id));
                _viewModel.Customers[index] = _viewModel.CurrentCustomer;
                _viewModel.AllCustomers = _viewModel.Customers.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            _viewModel.CurrentCustomer = new CustomerModel();
            _viewModel.SelectedCustomer = null;
            _viewModel.CurrentState = State.NORMAL;
        }
    }
}

