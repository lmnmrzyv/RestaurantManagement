using DataAccessManager.Domain.Entities;
using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Mappers
{
    public class DepartmentMapper : IMapper<DepartmentsModel, Department>
    {
        public DepartmentsModel Map(Department entity)
        {
            var departmentModel = new DepartmentsModel();

            departmentModel.Name = entity.Name;
            

            return departmentModel;
        }

        public Department Map(DepartmentsModel model)
        {
            var department = new Department();

            department.Name = model.Name;
           

            return department;
        }
    }
}
