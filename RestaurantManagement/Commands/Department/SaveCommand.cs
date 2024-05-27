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

namespace RestaurantManagement.Commands.Departments
{
    public class SaveCommand : ICommand
    {
        private readonly DepartmentsViewModel _viewModel;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db,DepartmentsViewModel viewModel)
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
            var mapper = new DepartmentMapper();
            var department = mapper.MapModelToEntity(new Department(), _viewModel.CurrentDepartment);
            if (department.Id == 0)
            {
                _viewModel.CurrentDepartment.Id = _db.DepartmentRepository.Add(department);
                var lastElementNo = _viewModel.Departments.LastOrDefault()?.No ?? 0;
                _viewModel.CurrentDepartment.No = lastElementNo + 1;
                _viewModel.Departments.Add(_viewModel.CurrentDepartment);
                _viewModel.AllDepartments = _viewModel.Departments.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var existingDepartment = _db.DepartmentRepository.Get(department.Id);
                _db.DepartmentRepository.Update(department);
                var index = _viewModel.Departments.IndexOf(_viewModel.Departments.First(x => x.Id == department.Id));
                _viewModel.Departments[index] = _viewModel.CurrentDepartment;
                _viewModel.AllDepartments = _viewModel.Departments.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            _viewModel.CurrentDepartment = new DepartmentModel();
            _viewModel.SelectedDepartment = null;
            _viewModel.CurrentState = State.NORMAL;
        }

    }
}
