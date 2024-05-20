using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Tables
{
    public class RejectCommand : ICommand
    {
        private readonly TablesViewModel _viewModel;
        public RejectCommand(TablesViewModel viewModel)
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
            _viewModel.SelectedTables = null;
            _viewModel.CurrentTables = new TablesModel();
            _viewModel.CurrentState = State.NORMAL;
            
        }
    }
}
