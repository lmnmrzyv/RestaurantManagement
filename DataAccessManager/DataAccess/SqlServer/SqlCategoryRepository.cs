using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessManager.Domain.SqlServer
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;
        public SqlCategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Add(Category item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Categories (name,IsActive)
                               output inserted.Id VALUES (@name,1);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", item.name);

                    return (int)command.ExecuteScalar();
                }
            }

        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "Update Categories SET ISActive=0 where id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Category Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, name,IsActive FROM Category WHERE Id = @Id and IsActive=1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() == false)
                            return null;

                        Category category = new Category();

                        category.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        category.name = reader.GetString(reader.GetOrdinal("name"));
                        category.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));

                        return category;
                    }
                }
            }
        }

        public List<Category> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Categories where IsActive=1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    List<Category> Categories = new List<Category>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category category = new Category();

                            category.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            category.name = reader.GetString(reader.GetOrdinal("name"));
                            category.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                            
                            Categories.Add(category);
                        }

                        return Categories;
                    }
                }
            }
        }

        public void Update(Category item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Categories SET name = @name WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", item.name);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

