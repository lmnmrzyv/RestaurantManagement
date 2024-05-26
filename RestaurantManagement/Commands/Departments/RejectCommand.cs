using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Departments
{
    public class RejectCommand : ICommand
    {
        private readonly DepartmentsViewModel _viewModel;
        public RejectCommand(DepartmentsViewModel viewModel)
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
            _viewModel.SelectedDepartments = null;
            _viewModel.CurrentDepartments = new DepartmentModel();
            _viewModel.CurrentState = State.NORMAL;
            
        }
    }
}
