using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlOrderRepository : IOrderRepository
    {
        private readonly string _connectionString;
        public SqlOrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Add(Order item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Orders (OrderTime, TotalPrice, PaymentMethod)
                               output inserted.Id VALUES (@OrderTime, @TotalPrice, @PaymentMethod);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderTime,", item.OrderTime);
                    command.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
                    command.Parameters.AddWithValue("@PaymentMethod", item.PaymentMethod);

                    return (int)command.ExecuteScalar();
                }
            }

        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "delete from Orders where Id=@Id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }


        public Order Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, OrderTime, TotalPrice, PaymentMethod FROM Orders WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() == false)
                            return null;

                        Order order = new Order();

                        order.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        order.OrderTime = reader.GetDateTime(reader.GetOrdinal("OrderTime"));
                        order.TotalPrice = reader.GetDecimal(reader.GetOrdinal("TotalPrice"));
                        order.PaymentMethod = reader.GetString(reader.GetOrdinal("PaymentMethod"));



                        return order;
                    }
                }
            }
        }

        public List<Order> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, OrderTime, TotalPrice, PaymentMethod FROM Order";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    List<Order> Orders = new List<Order>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order();

                            order.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            order.OrderTime = reader.GetDateTime(reader.GetOrdinal("OrderTime"));
                            order.TotalPrice = reader.GetDecimal(reader.GetOrdinal("TotalPrice"));
                            order.PaymentMethod = reader.GetString(reader.GetOrdinal("PaymentMethod"));

                            Orders.Add(order);
                        }

                        return Orders;
                    }
                }
            }
        }

        public void Update(Order item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Orders SET OrderTime=@OrderTime, TotalPrice=@TotalPrice, PaymentMethod=@PaymentMethod WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderTime,", item.OrderTime);
                    command.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
                    command.Parameters.AddWithValue("@PaymentMethod", item.PaymentMethod);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
