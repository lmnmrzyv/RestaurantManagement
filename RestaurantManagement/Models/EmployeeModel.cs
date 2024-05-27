using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.EnumsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class EmployeeModel : IModel
    {
        public int Id { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public PositionsModel Position { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public decimal PerformanceRating { get; set; }
    }
}
