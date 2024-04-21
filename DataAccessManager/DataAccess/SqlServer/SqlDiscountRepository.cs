using DataAccessManager.Domain.Entities;
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

                string query = @"INSERT INTO Discounts (startTime,endTime,Status, CategoryId,percent)
                               output inserted.Id VALUES (@startTime,@endTime,@Status, @CategoryId,@percent);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@startTime", item.startTime);
                    command.Parameters.AddWithValue("@endTime", item.endTime);
                    command.Parameters.AddWithValue("@Status", item.Status);
                    command.Parameters.AddWithValue("@CategoryId", item.CategoryId);
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

                string query = "UPDATE Discounts SET Status = 0 WHERE Id = @Id";

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

                string query = "SELECT Id, startTime, endTime, Status, CategoryId, Percent FROM Discounts WHERE Id = @Id and Status = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() == false)
                            return null;

                        Discount Discount = new Discount();

                        Discount.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        Discount.startTime = reader.GetDateTime(reader.GetOrdinal("startTime"));
                        Discount.endTime = reader.GetDateTime(reader.GetOrdinal("endTime"));
                        Discount.Status = reader.GetBoolean(reader.GetOrdinal("Status"));
                        Discount.CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                        Discount.Percent = reader.GetInt32(reader.GetOrdinal("Percent"));

                        return Discount;
                    }
                }
            }
        }

        public List<Discount> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, startTime, endTime, Status, CategoryId, Percent FROM Discounts WHERE Status = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    List<Discount> discounts = new List<Discount>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Discount discount = new Discount();
                            discount.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            discount.startTime = reader.GetDateTime(reader.GetOrdinal("startTime"));
                            discount.endTime = reader.GetDateTime(reader.GetOrdinal("endTime"));
                            discount.Status = reader.GetBoolean(reader.GetOrdinal("Status"));
                            discount.CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                            discount.Percent = reader.GetInt32(reader.GetOrdinal("Percent"));

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

                string query = "UPDATE Discounts SET startTime = @startTime, endTime = @endTime, Status = @Status,CategoryId = @CategoryId, Percent= @Percent WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@startTime", item.startTime);
                    command.Parameters.AddWithValue("@endTime", item.endTime);
                    command.Parameters.AddWithValue("@Status", item.Status);
                    command.Parameters.AddWithValue("@CategoryId", item.CategoryId);
                    command.Parameters.AddWithValue("@Percent", item.Percent);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
