using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlTableRepository : ITableRepository
    {
        private readonly string _connectionString;
        public SqlTableRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(Table item)
        {
            using(SqlConnection connection=new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Tables (TableNumber, Capacity) OUTPUT INSERTED.Id VALUES (@TableNumber, @Capacity);";
                using(SqlCommand command= new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableNumber", item.TableNumber);
                    command.Parameters.AddWithValue("@Capacity", item.Capacity);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Tables SET IsActive = 0 WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Table Get(int id)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"Select Id,TableNumber,Capacity from Tables where Id=@Id";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                       Table table = new Table();

                        table.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        table.TableNumber = reader.GetInt32(reader.GetOrdinal("TableNumber"));
                        table.Capacity = reader.GetInt32(reader.GetOrdinal("Capacity"));

                        return table;
                    }

                }

            }
        }

        public List<Table> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                List<Table> tables = new List<Table>();

                string query = "SELECT * FROM Tables";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataReader Reader = cmd.ExecuteReader();

                    while (Reader.Read())
                    {
                        Table table = new Table();
                        table.Id = (int)Reader["Id"];
                        table.TableNumber = (int)Reader["TableNumber"];
                        table.Capacity = (int)Reader["Capacity"];


                        tables.Add(table);


                    }
                }

                return tables;
            }
        }

        public void Update(Table item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"UPDATE Tables SET TableNumber=@TableNumber, Capacity=@Capacity where Id=@Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@TableNumber", item.TableNumber);
                    command.Parameters.AddWithValue("@Capacity", item.Capacity);
                    command.ExecuteNonQuery();
                }

            }
        }
    }
}
