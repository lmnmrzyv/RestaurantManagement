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
using System.Windows.Input;

namespace RestaurantManagement.Commands.Departments
{
    public class SaveCommand : ICommand
    {
        private readonly DepartmentsViewModel _currentState;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db,DepartmentsViewModel currentState)
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
            var mapper = new DepartmentMapper();
            var departments = mapper.MapModelToEntity(new Department(), _currentState.CurrentDepartments);

            _db.DepartmentRepository.Add(departments);

            var lastElementNo=_currentState.Departments.LastOrDefault()?.No ?? 0;

            _currentState.CurrentDepartments.No = lastElementNo+1;

            _currentState.Departments.Add(_currentState.CurrentDepartments);
            _currentState.CurrentDepartments = new DepartmentsModel();

            _currentState.CurrentState = State.NORMAL;
        }
    }
}
