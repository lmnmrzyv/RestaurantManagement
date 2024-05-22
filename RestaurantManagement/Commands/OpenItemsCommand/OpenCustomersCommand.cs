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
    public class OpenCustomersCommand:ICommand
    {

        public event EventHandler CanExecuteChanged;
        private readonly IUnitOfWork _db;
        public OpenCustomersCommand(IUnitOfWork db)
        {
            _db = db;
        }
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

            var customerControl = new CustomersControl();
            var viewModel = new CustomersViewModel(_db);

            var customers = _db.CustomerRepository.GetAll();

            var customersModel = new List<CustomersModel>();
            var customerMapper = new CustomerMapper();
            var no = 1;

            foreach (var customer in customers)
            {
                var customerModel = customerMapper.MapEntityToModel(customer, new CustomersModel());

                customerModel.No = no++;

                customersModel.Add(customerModel);
            }

            viewModel.Customers = new ObservableCollection<CustomersModel>(customersModel);
            customerControl.DataContext = viewModel;

            grid.Children.Add(customerControl);

        }
    }
}
