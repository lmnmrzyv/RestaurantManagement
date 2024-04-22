using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlOrderDetailRepository : IOrderDetailRepository
    {
        private readonly string _connectionString;
        public SqlOrderDetailRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Add(OrderDetail item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO OrderDetails (OrderId,MenuItemId,Quantity,Amount) output inserted.Id VALUES (@OrderId,@MenuItemId,
                               @Quantity,@Amount)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", item.OrderId);
                    command.Parameters.AddWithValue("@MenuItemId", item.MenuItemId);
                    command.Parameters.AddWithValue("@Quantity", item.Quantity);
                    command.Parameters.AddWithValue("@Amount", item.Amount);

                    return (int)command.ExecuteScalar();
                }

            }
        }

        public void Delete(OrderDetail item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"DELETE OrderDetails where Id=@Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.ExecuteNonQuery();
                }

            }
        }

        public OrderDetail Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"Select Id,OrderId,MenuItemId,Quantity,Amount from OrderDetails where Id=@Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        orderDetail.OrderId = reader.GetInt32(reader.GetOrdinal("OrderId"));
                        orderDetail.MenuItemId = reader.GetInt32(reader.GetOrdinal("MenuItemId"));
                        orderDetail.Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                        orderDetail.Amount = reader.GetDecimal(reader.GetOrdinal("Amount"));

                        return orderDetail;
                    }
                }

            }
        }

        public List<OrderDetail> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                List<OrderDetail> orderDetails = new List<OrderDetail>();
                string query = "SELECT *FROM OrderDetails";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        orderDetail.OrderId = reader.GetInt32(reader.GetOrdinal("OrderId"));
                        orderDetail.MenuItemId = reader.GetInt32(reader.GetOrdinal("MenuItemId"));
                        orderDetail.Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                        orderDetail.Amount = reader.GetDecimal(reader.GetOrdinal("Amount"));

                        orderDetails.Add(orderDetail);
                    }
                    return orderDetails;
                }

            }
        }

        public void Update(OrderDetail item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"UPDATE OrderDetails SET OrderId=@OrderId,MenuItemId=@MenuItemId,Quantity=@Quantity, Amount=@Amount where Id=@Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@OrderId", item.OrderId);
                    command.Parameters.AddWithValue("@MenuItemId", item.MenuItemId);
                    command.Parameters.AddWithValue("@Quantity", item.Quantity);
                    command.Parameters.AddWithValue("@Amount", item.Amount);
                    command.ExecuteNonQuery();
                }

            }
        }
    }
}
