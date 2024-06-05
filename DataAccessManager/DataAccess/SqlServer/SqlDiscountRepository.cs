using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.EnumsDB;
using DataAccessManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlDiscountRepository : IDiscountRepository
    {
        private readonly string _connectionString;
        public SqlDiscountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(Discount item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Discounts (startTime,endTime, CategoryId,[Percent],IsActive)
                               output inserted.Id VALUES (@startTime,@endTime, @CategoryId,@percent,1);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@startTime", item.startTime);
                    command.Parameters.AddWithValue("@endTime", item.endTime);
                    command.Parameters.AddWithValue("@CategoryId", item.Category.Id);
                    command.Parameters.AddWithValue("@percent", item.Percent);

                    return (int)command.ExecuteScalar();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Discounts SET IsActive = 0 WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Discount Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"SELECT d.Id, d.startTime, d.endTime, d.[Percent], d.IsActive,
                                c.Id AS CategoryId, c.Name AS CategoryName
                         FROM Discounts AS d
                         INNER JOIN Categories AS c ON d.CategoryId = c.Id
                         WHERE d.Id = @Id AND d.IsActive = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        Discount discount = new Discount();

                        discount.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        discount.startTime = reader.GetDateTime(reader.GetOrdinal("startTime"));
                        discount.endTime = reader.GetDateTime(reader.GetOrdinal("endTime"));
                        discount.Percent = reader.GetDouble(reader.GetOrdinal("Percent"));
                        discount.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        discount.Category = new Category
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                            name = reader.GetString(reader.GetOrdinal("CategoryName"))
                        };

                        return discount;
                    }
                }
            }
        }

        public List<Discount> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"SELECT d.Id AS DiscountId, d.startTime, d.endTime, d.[Percent], d.IsActive ,
                                           c.Id AS CategoryId, c.Name AS CategoryName
                                       From Discounts AS d
                                       Inner Join Categories AS c On d.CategoryId = c.Id
                                       where d.IsActive=1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    List<Discount> discounts = new List<Discount>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Discount discount = new Discount();
                            discount.Id = reader.GetInt32(reader.GetOrdinal("DiscountId"));
                            discount.startTime = reader.GetDateTime(reader.GetOrdinal("startTime"));
                            discount.endTime = reader.GetDateTime(reader.GetOrdinal("endTime"));
                            discount.Percent = reader.GetDouble(reader.GetOrdinal("Percent"));
                            discount.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                            discount.Category = new Category
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                name = reader.GetString(reader.GetOrdinal("CategoryName"))
                            };
                            discounts.Add(discount);
                        }

                        return discounts;
                    }
                }
            }
        }

        public void Update(Discount item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Discounts SET startTime = @startTime, endTime = @endTime, CategoryId = @CategoryId, [Percent]= @Percent WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@startTime", item.startTime);
                    command.Parameters.AddWithValue("@endTime", item.endTime);
                    command.Parameters.AddWithValue("@CategoryId", item.Category.Id);
                    command.Parameters.AddWithValue("@Percent", item.Percent);

                    command.ExecuteNonQuery();
                }
          
            }
        }

    }
}
