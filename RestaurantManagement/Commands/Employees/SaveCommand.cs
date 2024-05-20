using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Mappers;
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
    public class SaveCommand : ICommand
    {
        private readonly EmployeesViewModel _currentState;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db,EmployeesViewModel currentState)
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
            var mapper = new EmployeeMapper();
            var employees =  mapper.MapModelToEntity(new Employee(), _currentState.CurrentEmployees);

            _db.EmployeeRepository.Add(employees);

            var lastElementNo = _currentState.Employees.LastOrDefault()?.No ?? 0;

            _currentState.CurrentEmployees.No=lastElementNo+1;
            _currentState.Employees.Add(_currentState.CurrentEmployees);
            _currentState.CurrentEmployees = new EmployeesModel();

            _currentState.CurrentState = Enums.State.NORMAL;
        }
    }
}
