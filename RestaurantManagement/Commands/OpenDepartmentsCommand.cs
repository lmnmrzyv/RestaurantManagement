using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using RestaurantManagement.Views.Controls;
using System;
using System.Collections.Generic;
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
            var viewModel = new DepartmentsViewModel();

            var departments = _db.DepartmentRepository.GetAll();

            var departmentsModel = new List<DepartmentsModel>();

            var departmentsMapper = new DepartmentMapper();
            var no = 1;

            foreach (var department in departments)
            {
                var departmentModel = departmentsMapper.Map(department);

                departmentModel.No = no++;

                departmentsModel.Add(departmentModel);
            }

            viewModel.Departments = departmentsModel;

            control.DataContext = viewModel;

            grid.Children.Add(control);
        }
    }
}
