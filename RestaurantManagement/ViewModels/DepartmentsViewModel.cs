using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.ViewModels
{
    public class DepartmentsViewModel
    {
        public DepartmentsViewModel()
        {
            
        }

        public List<DepartmentsModel> Departments { get; set; }
    }
}
