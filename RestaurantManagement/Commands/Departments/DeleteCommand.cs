using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Departments
{
    public class DeleteCommand : ICommand
    {
        private readonly IUnitOfWork _db;
        private readonly DepartmentsViewModel _currentState;
        public DeleteCommand(IUnitOfWork db,DepartmentsViewModel currentState)
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

            var deletedId = _currentState.SelectedDepartments.Id;

            _db.DepartmentRepository.Delete(deletedId);

            MessageBox.Show("Successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _currentState.Departments.Remove(_currentState.SelectedDepartments);

            for (int i = 0; i < _currentState.Departments.Count; i++)
            {
                _currentState.Departments[i].No = i + 1;
            }
        }
    }
}
