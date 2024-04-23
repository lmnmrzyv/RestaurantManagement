using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.Domain.Entities
{
    public class Employee:IDbEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int PositionId { get; set; }

        public string EducationLevel { get; set; }


        public bool IsActive { get; set; }
        
       
    }
}