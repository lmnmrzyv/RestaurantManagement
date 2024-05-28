using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Discounts
{
    public class RejectCommand : ICommand
    {
        private readonly DiscountsViewModel _viewModel;
        public RejectCommand(DiscountsViewModel viewModel)
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
            _viewModel.SelectedDiscount = null;
            _viewModel.CurrentDiscount = new DiscountModel();
            _viewModel.CurrentState = State.NORMAL;

        }
    }
}
