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
        private readonly EmployeesViewModel _viewModel;
        public DeleteCommand(IUnitOfWork db, EmployeesViewModel viewModel)
        {
            _db = db;
            _viewModel = viewModel;
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

            var deletedId = _viewModel.SelectedEmployee.Id;
            _db.EmployeeRepository.Delete(deletedId);
            MessageBox.Show("Successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _viewModel.Employees.Remove(_viewModel.SelectedEmployee);
            _viewModel.AllEmployees = _viewModel.Employees.ToList();

            for (int i = 0; i < _viewModel.Employees.Count; i++)
            {
                _viewModel.Employees[i].No = i + 1;
            }
            _viewModel.Employees = new ObservableCollection<EmployeeModel>(_viewModel.Employees);
            _viewModel.CurrentState = State.NORMAL;
        }
    }

}
