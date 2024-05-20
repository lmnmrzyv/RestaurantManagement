using RestaurantManagement.Enums;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Tables
{
    public class AddCommand : ICommand
    {
        private readonly TablesViewModel _currentState;
        public AddCommand(TablesViewModel currentState) 
        {
            _currentState=currentState;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _currentState.CurrentState = State.CREATED;
        }
    }
}
