using DataAccessManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class EmployeesModel : IModel
    {
        public int No { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Position Position { get; set; }
        public string EducationLevel { get; set; }
        public decimal PerformanceRating { get; set; }
    }
}
