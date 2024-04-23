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
                    command.Parameters.AddWithValue("@DepartmentId", item.DepartmentId);
                  //  command.Parameters.AddWithValue("@IsActive", item.IsActive);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public void Delete(Position item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Positions SET IsActive = 0 WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.ExecuteNonQuery();
                }

            }
        }

        public Position Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"Select Id,Name,DepartmentId from Positions where Id=@Id and IsActive=1";

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
                        position.DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId"));

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

                string query = "SELECT * FROM Positions where IsActive=1";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataReader Reader = cmd.ExecuteReader();

                    while (Reader.Read())
                    {
                        Position position = new Position();
                        position.Id = (int)Reader["Id"];
                        position.Name = (string)Reader["Name"];
                        position.DepartmentId = (int)Reader["DepartmentId"];


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
                    command.Parameters.AddWithValue("@DepartmentId", item.DepartmentId);
                    command.ExecuteNonQuery();
                }

            }
        }
    }
}
