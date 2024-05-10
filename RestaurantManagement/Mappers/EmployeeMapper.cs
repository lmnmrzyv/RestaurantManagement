using DataAccessManager.Domain.Entities;
using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Mappers
{
    public class EmployeeMapper : IMapper<EmployeesModel, Employee>
    {
        public EmployeesModel Map(Employee entity)
        {
           var employeeModel = new EmployeesModel();
           employeeModel.EducationLevel = entity.EducationLevel;
           employeeModel.PerformanceRating = entity.PerformanceRating;
           employeeModel.Surname = entity.Surname;
           employeeModel.Name = entity.Name;
           employeeModel.Position = entity.Position;

            return employeeModel;
        }

        public Employee Map(EmployeesModel model)
        {
            var employee=new Employee();
            employee.Name = model.Name;
            employee.Position = model.Position;
            employee.Surname= model.Surname;
            employee.PerformanceRating = model.PerformanceRating;
            employee.EducationLevel= model.EducationLevel;

            return employee;
        }
    }
}
