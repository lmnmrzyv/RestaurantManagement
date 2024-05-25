using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Employees
{
    public class DeleteCommand : ICommand
    {
        private readonly IUnitOfWork _db;
        private readonly EmployeesViewModel _currentState;
        public DeleteCommand(IUnitOfWork db, EmployeesViewModel currentState)
        {
            _db = db;
            _currentState = currentState;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var result = MessageBox.Show("Are you sure you want to delete selected item?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            var deletedId = _currentState.SelectedEmployees.Id;

            _db.EmployeeRepository.Delete(deletedId); // Corrected to EmployeeRepository

            MessageBox.Show("Successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _currentState.Employees.Remove(_currentState.SelectedEmployees);

            for (int i = 0; i < _currentState.Employees.Count; i++)
            {
                _currentState.Employees[i].No = i + 1;
            }
            _currentState.Employees = new ObservableCollection<EmployeesModel>(_currentState.Employees);
            _currentState.CurrentState = State.NORMAL;
        }
    }

}
