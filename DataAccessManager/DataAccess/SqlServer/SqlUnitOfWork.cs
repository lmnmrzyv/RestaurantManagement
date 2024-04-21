using DataAccessManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository => new SqlEmployeeRepository();

        public IPositionRepository PositionRepository => new SqlPositionRepository();

        public IDepartmentRepository DepartmentRepository => new SqlDepartmentRepository();

        public IReservationRepository ReservationRepository => new SqlReservationRepository();
    }
}
