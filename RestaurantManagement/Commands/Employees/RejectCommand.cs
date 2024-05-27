using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Employees
{
    public class RejectCommand : ICommand
    {
        private readonly EmployeesViewModel _currentState;
        public RejectCommand(EmployeesViewModel currentState)
        {
            _currentState = currentState;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _currentState.SelectedEmployee = null;
            _currentState.CurrentEmployee=new EmployeeModel();
            _currentState.CurrentState = State.NORMAL;
        }
    }
}
