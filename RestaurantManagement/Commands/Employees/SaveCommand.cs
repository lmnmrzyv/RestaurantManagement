using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Employees
{
    public class SaveCommand : ICommand
    {
        private readonly EmployeesViewModel _viewModel;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db,EmployeesViewModel viewModel)
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
            var mapper = new EmployeeMapper();
            var employee = mapper.MapModelToEntity(new Employee(), _viewModel.CurrentEmployee);
            employee.IsActive=true;
            if (employee.Id == 0)
            {
                _viewModel.CurrentEmployee.Id = _db.EmployeeRepository.Add(employee);
                var lastElementNo = _viewModel.AllEmployees.LastOrDefault()?.No ?? 0;
                _viewModel.CurrentEmployee.No = lastElementNo + 1;
                _viewModel.Employees.Add(_viewModel.CurrentEmployee);
                _viewModel.AllEmployees = _viewModel.Employees.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var existingDepartment = _db.EmployeeRepository.Get(employee.Id);
                _db.EmployeeRepository.Update(employee);
                var index = _viewModel.Employees.IndexOf(_viewModel.Employees.First(x => x.Id == employee.Id));
                _viewModel.Employees[index] = _viewModel.CurrentEmployee;
                _viewModel.AllEmployees = _viewModel.Employees.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            _viewModel.CurrentEmployee = new EmployeeModel();
            _viewModel.SelectedEmployee = null;
            _viewModel.CurrentState = State.NORMAL;
        }
    }
}
