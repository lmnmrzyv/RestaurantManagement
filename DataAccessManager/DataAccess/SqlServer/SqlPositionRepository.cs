using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlPositionRepository : IPositionRepository
    {
        private readonly string _connectionString;
        public SqlPositionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Add(Position item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Positions (Name, DepartmentId,IsActive) OUTPUT INSERTED.Id VALUES (@Name, @DepartmentId,1);";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@DepartmentId", item.Department.Id);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Positions SET IsActive = 0 WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }

            }
        }

        public Position Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"SELECT p.Id, p.Name, p.IsActive,
                                d.Id AS DepartmentId, d.Name AS DepartmentName
                         FROM Positions AS p
                         INNER JOIN Departments AS d ON p.DepartmentId = d.Id
                         WHERE p.Id = @Id AND p.IsActive = 1";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        Position position = new Position();

                        position.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        position.Name = reader.GetString(reader.GetOrdinal("Name"));
                        position.Department = new Department
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            Name = reader.GetString(reader.GetOrdinal("DepartmentName"))
                        };
                        position.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        return position;
                    }

                }

            }
        }

        public List<Position> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                List<Position> positions = new List<Position>();

                string query = @"SELECT p.Id AS PositionId, p.Name, p.IsActive,
                                d.Id AS DepartmentId, d.Name AS DepartmentName
                         FROM Positions AS p
                         INNER JOIN Departments AS d ON p.DepartmentId = d.Id
                         WHERE p.IsActive = 1";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Position position = new Position();
                        position.Id = (int)reader["PositionId"];
                        position.Name = (string)reader["Name"];
                        position.Department = new Department
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            Name = reader.GetString(reader.GetOrdinal("DepartmentName"))
                        };
                        position.IsActive = (bool)reader["IsActive"];

                        positions.Add(position);
                    }
                }

                return positions;
            }
        }

        public void Update(Position item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"UPDATE Positions SET Name=@Name,DepartmentId=@DepartmentId where Id=@Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@DepartmentId", item.Department.Id);
                    command.ExecuteNonQuery();
                }

            }
        }
    }
}
