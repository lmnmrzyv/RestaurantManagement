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
    public class OpenEmployeesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly IUnitOfWork _db;

        public OpenEmployeesCommand(IUnitOfWork db)
        {
            _db = db;
            
        }
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

            var employeesControl = new EmployeesControl();
            var viewModel = new EmployeesViewModel(_db);
            viewModel.LoadPositions();
            

            var employees=_db.EmployeeRepository.GetAll();

            var employeesModel=new List<EmployeeModel>();
            var employeesMapper = new EmployeeMapper();
            var no = 1;

            foreach ( var employee in employees)
            {
                var employeeModel=employeesMapper.MapEntityToModel(employee,new EmployeeModel());

                employeeModel.No = no++;

                employeesModel.Add(employeeModel);
            }

            viewModel.AllEmployees = employeesModel;
            viewModel.Employees= new ObservableCollection<EmployeeModel>(employeesModel);

            employeesControl.DataContext = viewModel;

            grid.Children.Add(employeesControl);
        }
    }
}
