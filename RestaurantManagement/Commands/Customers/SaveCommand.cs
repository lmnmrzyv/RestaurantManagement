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
using System.Windows.Input;

namespace RestaurantManagement.Commands.Customers
{
    public class SaveCommand : ICommand
    {
        private readonly CustomersViewModel _currentState;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, CustomersViewModel currentState)
        {
            _db = db;
            _currentState = currentState;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var mapper = new CustomerMapper();
            var customers = mapper.MapModelToEntity(new Customer(), _currentState.CurrentCustomers);

            _db.CustomerRepository.Add(customers);

            var lastElementNo = _currentState.Customers.LastOrDefault()?.No ?? 0;

            _currentState.CurrentCustomers.No = lastElementNo + 1;

            _currentState.Customers.Add(_currentState.CurrentCustomers);
            _currentState.CurrentCustomers = new CustomersModel();

            _currentState.CurrentState = State.NORMAL;
        }
    }
}

