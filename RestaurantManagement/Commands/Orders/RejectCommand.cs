using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Orders
{
    public class RejectCommand : ICommand
    {
        private readonly OrdersViewModel _viewModel;
        public RejectCommand(OrdersViewModel viewModel)
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
            _viewModel.SelectedOrder = null;
            _viewModel.CurrentOrder = new OrdersModel();
            _viewModel.CurrentState = State.NORMAL;

        }
    }
}

   
