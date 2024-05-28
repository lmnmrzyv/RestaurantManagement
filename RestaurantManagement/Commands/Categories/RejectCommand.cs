using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Categories
{
    public class RejectCommand : ICommand
    {
        private readonly CategoriesViewModel _viewModel;
        public RejectCommand(CategoriesViewModel viewModel)
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
            _viewModel.SelectedCategory = null;
            _viewModel.CurrentCategory = new CategoriesModel();
            _viewModel.CurrentState = State.NORMAL;

        }
    }
}
