using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;
        public SqlEmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Add(Employee item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Employees (Name,Surname,PositionId,EducationLevel,PerformanceRating,IsActive) output inserted.Id VALUES (@Name,@Surname,
                               @PositionId,@EducationLevel,@PerformanceRating,1)";

                using (SqlCommand command = new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Surname", item.Surname);
                    command.Parameters.AddWithValue("@PositionId", item.PositionId);
                    command.Parameters.AddWithValue("@EducationLevel", item.EducationLevel ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PerformanceRating", item.PerformanceRating);

                    return (int)command.ExecuteScalar();
                }
                
            }
        }
        public void Update(Employee item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"UPDATE Employees SET Name=@Name,Surname=@Surname,PositionId=@PositionId
                 , EducationLevel=@EducationLevel,PerformanceRating=@PerformanceRating where Id=@Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Surname", item.Surname);
                    command.Parameters.AddWithValue("@PositionId", item.PositionId);
                    command.Parameters.AddWithValue("@EducationLevel", item.EducationLevel ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PerformanceRating", item.PerformanceRating);
                    command.ExecuteNonQuery();
                }

            }
        }
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"UPDATE Employees SET IsActive=0 where Id=@Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }

            }
        }

        public Employee Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"Select Id,Name,Surname,PositionId,EducationLevel,PerformanceRating from Employees where Id=@Id and IsActive=1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if(!reader.Read())
                            return null;

                        Employee employee = new Employee();
                        employee.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        employee.Name = reader.GetString(reader.GetOrdinal("Name"));
                        employee.Surname = reader.GetString(reader.GetOrdinal("Surname"));
                        employee.PositionId = reader.GetInt32(reader.GetOrdinal("PositionId"));
                        employee.EducationLevel = reader.IsDBNull(reader.GetOrdinal("EducationLevel")) ? null : reader.GetString(reader.GetOrdinal("EducationLevel"));
                        employee.PerformanceRating = reader.GetDecimal(reader.GetOrdinal("PerformanceRating"));

                        return employee;
                    }
                }

            }
        }

        public List<Employee> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                List<Employee> employees = new List<Employee>();
                string query = @"Select Id,Name,Surname,PositionId,EducationLevel,PerformanceRating from Employees where  IsActive=1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        employee.Name = reader.GetString(reader.GetOrdinal("Name"));
                        employee.Surname = reader.GetString(reader.GetOrdinal("Surname"));
                        employee.PositionId = reader.GetInt32(reader.GetOrdinal("PositionId"));
                        employee.EducationLevel = reader.IsDBNull(reader.GetOrdinal("EducationLevel")) ? null : reader.GetString(reader.GetOrdinal("EducationLevel"));
                        employee.PerformanceRating = reader.GetDecimal(reader.GetOrdinal("PerformanceRating"));

                        employees.Add(employee);
                    }
                    return employees;
                }

            }
        }

        
    }
}
