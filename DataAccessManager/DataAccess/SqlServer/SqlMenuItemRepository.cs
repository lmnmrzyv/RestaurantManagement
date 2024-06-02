using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.EnumsDB;
using DataAccessManager.Domain.Interfaces;
using System;
using System.Collections;
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

                string query = @"INSERT INTO MenuItems (name, description,CategoryId, price,IsActive)
                               output inserted.Id VALUES (@name, @description,@CategoryId, @price,1);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", item.name);
                    command.Parameters.AddWithValue("@description", item.description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CategoryId", item.Category.Id);
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

                string query = @"SELECT m.Id, m.name, m.Surname, m.description, m.price, m.IsActive,
                                c.Id AS CategoryId, c.Name AS CategoryName
                         FROM MenuItems AS m
                         INNER JOIN Categories AS c ON m.CategoryId = c.Id
                         WHERE m.Id = @Id AND m.IsActive = 1";

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
                        menuitem.price = reader.GetFloat(reader.GetOrdinal("price"));
                        menuitem.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        menuitem.Category = new Category
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                            name = reader.GetString(reader.GetOrdinal("CategoryName"))
                        };


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

                List<MenuItem> menuItems = new List<MenuItem>();

                string query = @"SELECT m.Id AS MenuItemId, m.name, m.description, m.price, m.IsActive,
                                c.Id AS CategoryId, c.name AS CategoryName
                         FROM MenuItems AS m
                         INNER JOIN Categories AS c ON m.CategoryId = c.Id
                         WHERE m.IsActive = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MenuItem menuitem = new MenuItem();

                            menuitem.Id = reader.GetInt32(reader.GetOrdinal("MenuItemId"));
                            menuitem.name = reader.GetString(reader.GetOrdinal("name"));
                            menuitem.description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description"));
                            menuitem.price = reader.GetDouble(reader.GetOrdinal("price"));
                            menuitem.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                            menuitem.Category = new Category
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                name = reader.GetString(reader.GetOrdinal("CategoryName"))
                            };

                            menuItems.Add(menuitem);
                        }


                        return menuItems;
                    }
                }
            }
        }


        public void Update(MenuItem item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE MenuItems SET name = @name, description=@description, CategoryId=@CategoryId, price=@price WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@name", item.name);
                    command.Parameters.AddWithValue("@descriprion", item.description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CategoryId", item.Category.Id);
                    command.Parameters.AddWithValue("@price", item.price);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

