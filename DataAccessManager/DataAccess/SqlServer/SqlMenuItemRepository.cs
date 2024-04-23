using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlMenuItemRepository : IMenuItemRepository
    {
        private readonly string _connectionString;
        public SqlMenuItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Add(MenuItem item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO MenuItems (name, description, price,IsActive)
                               output inserted.Id VALUES (@name, @description, @price,1);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name,", item.name);
                    command.Parameters.AddWithValue("@description", item.description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@price", item.price);

                    return (int)command.ExecuteScalar();
                }
            }

        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "Update MenuItems SET ISActive = 0 where id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }


        public MenuItem Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, name, description, price FROM MenuItems WHERE Id = @Id and IsActive=1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() == false)
                            return null;

                        MenuItem menuitem = new MenuItem();

                        menuitem.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        menuitem.name = reader.GetString(reader.GetOrdinal("name"));
                        menuitem.description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description"));
                        menuitem.price = reader.GetInt32(reader.GetOrdinal("price"));



                        return menuitem;
                    }
                }
            }
        }

        public List<MenuItem> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, name, description, price FROM MenuItems where IsActive=1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    List<MenuItem> MenuItems = new List<MenuItem>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MenuItem menuitem = new MenuItem();

                            menuitem.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            menuitem.name = reader.GetString(reader.GetOrdinal("name"));
                            menuitem.description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description"));
                            menuitem.price = reader.GetInt32(reader.GetOrdinal("price"));

                            MenuItems.Add(menuitem);
                        }

                        return MenuItems;
                    }
                }
            }
        }

        public void Update(MenuItem item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE MenuItems SET name = @name, description=@description, price=@price WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", item.name);
                    command.Parameters.AddWithValue("@descriprion", item.description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@price", item.price);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

