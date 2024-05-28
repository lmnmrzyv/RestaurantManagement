using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagement.Commands.MenuItems
{
    public class RejectCommand : ICommand
    {
        private readonly MenuItemsViewModel _viewModel;
        public RejectCommand(MenuItemsViewModel viewModel)
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
            _viewModel.SelectedMenuItem = null;
            _viewModel.CurrentMenuItem = new MenuItemsModel();
            _viewModel.CurrentState = State.NORMAL;

        }
    }
}
