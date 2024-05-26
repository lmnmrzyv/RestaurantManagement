using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public SqlUserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(User item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Name, Surname, Username, PasswordHash, IsActive  FROM Users WHERE Username = @Username and IsActive = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() == false)
                            return null;

                        User user = new User();

                        user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        user.Name = reader.GetString(reader.GetOrdinal("Name"));
                        user.Surname = reader.GetString(reader.GetOrdinal("Surname"));
                        user.Username = reader.GetString(reader.GetOrdinal("Username"));
                        user.PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                        user.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                       

                        return user;
                    }
                }
            }
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }

       
      
    }
}
