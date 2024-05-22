using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.EnumsDB;
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

                string query = @"INSERT INTO Employees (Name, Surname, PositionId, EducationLevel, PerformanceRating, IsActive) 
                         OUTPUT inserted.Id 
                         VALUES (@Name, @Surname, @PositionId, @EducationLevel, @PerformanceRating, 1)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Surname", item.Surname);
                    command.Parameters.AddWithValue("@PositionId", item.Position.Id);
                    command.Parameters.AddWithValue("@EducationLevel", item.EducationLevel.ToString()); // Enum to string
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

                string query = @"UPDATE Employees SET Name=@Name, Surname=@Surname, PositionId=@PositionId, 
                         EducationLevel=@EducationLevel, PerformanceRating=@PerformanceRating 
                         WHERE Id=@Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Surname", item.Surname);
                    command.Parameters.AddWithValue("@PositionId", item.Position.Id);
                    command.Parameters.AddWithValue("@EducationLevel", item.EducationLevel.ToString()); // Enum to string
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

                string query = @"UPDATE Employees SET IsActive=0 WHERE Id=@Id";

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

                string query = @"SELECT e.Id, e.Name, e.Surname, e.EducationLevel, e.PerformanceRating, e.IsActive,
                                p.Id AS PositionId, p.Name AS PositionName
                         FROM Employees AS e
                         INNER JOIN Positions AS p ON e.PositionId = p.Id
                         WHERE e.Id = @Id AND e.IsActive = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        Employee employee = new Employee
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Surname = reader.GetString(reader.GetOrdinal("Surname")),
                            PerformanceRating = reader.GetDecimal(reader.GetOrdinal("PerformanceRating")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                            Position = new Position
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("PositionId")),
                                Name = reader.GetString(reader.GetOrdinal("PositionName"))
                            }
                        };

                        string educationLevelString = reader.GetString(reader.GetOrdinal("EducationLevel"));
                        if (Enum.TryParse(educationLevelString, out EducationLevel educationLevel))
                        {
                            employee.EducationLevel = educationLevel;
                        }
                        else
                        {
                            employee.EducationLevel = EducationLevel.HighSchool; 
                        }

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

                string query = @"SELECT e.Id AS EmployeeId, e.Name, e.Surname, e.EducationLevel, e.PerformanceRating, e.IsActive,
                                p.Id AS PositionId, p.Name AS PositionName
                         FROM Employees AS e
                         INNER JOIN Positions AS p ON e.PositionId = p.Id
                         WHERE e.IsActive = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Surname = reader.GetString(reader.GetOrdinal("Surname")),
                                PerformanceRating = reader.GetDecimal(reader.GetOrdinal("PerformanceRating")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                Position = new Position
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("PositionId")),
                                    Name = reader.GetString(reader.GetOrdinal("PositionName"))
                                }
                            };

                            string educationLevelString = reader.GetString(reader.GetOrdinal("EducationLevel"));
                            if (Enum.TryParse(educationLevelString, out EducationLevel educationLevel))
                            {
                                employee.EducationLevel = educationLevel;
                            }
                            else
                            {
                                employee.EducationLevel = EducationLevel.HighSchool; 
                            }

                            employees.Add(employee);
                        }
                    }
                }

                return employees;
            }
        }

    }
}
