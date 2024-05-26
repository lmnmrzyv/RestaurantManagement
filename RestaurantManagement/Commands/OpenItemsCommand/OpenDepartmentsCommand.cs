using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using RestaurantManagement.Views.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RestaurantManagement.Commands
{
    public class OpenDepartmentsCommand:ICommand
    {
        private readonly IUnitOfWork _db;
        public OpenDepartmentsCommand(IUnitOfWork db)
        {
            _db = db;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var grid = parameter as Grid;

            if (grid == null)
                return;

            grid.Children.Clear();

            var control = new DepartmentsControl();
            var viewModel = new DepartmentsViewModel(_db);

            var departments = _db.DepartmentRepository.GetAll();

            var departmentsModel = new List<DepartmentModel>();

            var departmentsMapper = new DepartmentMapper();
            var no = 1;

            foreach (var department in departments)
            {
                var departmentModel = departmentsMapper.MapEntityToModel(department,new DepartmentModel());

                departmentModel.No = no++;

                departmentsModel.Add(departmentModel);
            }

            viewModel.AllDepartments = departmentsModel;
            viewModel.Departments = new ObservableCollection<DepartmentModel>(departmentsModel);

            control.DataContext = viewModel;

            grid.Children.Add(control);
        }
    }
}
