using RestaurantManagement.Enums;
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
    public class RejectCommand : ICommand
    {
        private readonly CustomersViewModel _viewModel;
        public RejectCommand(CustomersViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.SelectedCustomer = null;
            _viewModel.CurrentCustomer = new CustomerModel();
            _viewModel.CurrentState = State.NORMAL;

        }
    }
}
