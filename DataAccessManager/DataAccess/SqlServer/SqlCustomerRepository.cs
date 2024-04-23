using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Xml.Linq;


namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlCustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;
        public SqlCustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Add(Customer customer)
        {
            using(SqlConnection connection=new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"Insert into Customers (Name,Surname,PhoneNum,Mail) 
                                output inserted.Id Values(@Name,@Surname,@PhoneNum,@Mail);";
                using(SqlCommand command=new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@Surname", customer.Surname);
                    command.Parameters.AddWithValue("@PhoneNum", customer.PhoneNum);
                    command.Parameters.AddWithValue("@Mail", customer.Mail);
                    return (int)command.ExecuteScalar();
                }
            }
        }
        public void Delete(int id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "Update Customers SET ISActive=0 where id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }

        }
        public void Update(Customer customer)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "Update Customers Set Name=@Name,Surname=@Surname,PhoneNum=@PhoneNum,Mail=@Mail WHERE Id = @Id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name,", customer.Name);
                    command.Parameters.AddWithValue("@Surname", customer.Surname);
                    command.Parameters.AddWithValue("@PhoneNum", customer.PhoneNum);
                    command.Parameters.AddWithValue("@Mail",customer.Mail);

                    command.ExecuteNonQuery();
                }
            }

        }
        public Customer Get(int id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "Select Name,Surname,PhoneNum,Mail from Customers Where Id=@Id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() == false) return null;
                        Customer customer = new Customer();
                        customer.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        customer.Name = reader.GetString(reader.GetOrdinal("Name"));
                        customer.Surname = reader.GetString(reader.GetOrdinal("Surname"));
                        customer.PhoneNum = reader.GetString(reader.GetOrdinal("PhoneNum"));
                        customer.Mail = reader.IsDBNull(reader.GetOrdinal("Mail")) ? null : reader.GetString(reader.GetOrdinal("Mail"));
                        return customer;
                    }
                }
            }
        }
        public List<Customer> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "Select * from Customers";
                using (SqlCommand command = new SqlCommand(query,connection))
                {
                    List<Customer> customers= new List<Customer>();
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Customer customer = new Customer();
                            customer.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            customer.Name = reader.GetString(reader.GetOrdinal("Name"));
                            customer.Surname = reader.GetString(reader.GetOrdinal("Surname"));
                            customer.PhoneNum = reader.GetString(reader.GetOrdinal("PhoneNum"));
                            customer.Mail = reader.IsDBNull(reader.GetOrdinal("MAil")) ? null : reader.GetString(reader.GetOrdinal("Mail"));

                            customers.Add(customer);
                        }
                        return customers;

                    }
                }
                
            }
        }

    }
}
