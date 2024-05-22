using DataAccessManager.Domain.EnumsDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Entities
{
    public class Employee : IDbEntity
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Position Position { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public decimal PerformanceRating { get; set; }
        public bool IsActive { get; set; }
    }
}
