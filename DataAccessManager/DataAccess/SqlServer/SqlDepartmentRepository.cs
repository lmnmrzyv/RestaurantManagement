using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlDepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;
        public SqlDepartmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Add(Department item)
        {
            using(SqlConnection connection=new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Departments (Name,IsActive) OUTPUT INSERTED.Id VALUES (@Name,1)";

                using(SqlCommand command=new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@Name",item.Name);

                    return (int)command.ExecuteScalar();
                }
            }
            
        }
        public void Update(Department item)
        {
            using(SqlConnection connection=new SqlConnection(_connectionString))
            {
                connection.Open ();

                string query = @"UPDATE Departments SET Name=@Name where Id=@Id";

                using(SqlCommand command=new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.ExecuteNonQuery();
                }
                
            }
        }

        public void Delete(int id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"UPDATE Departments SET IsActive=0 where Id=@Id";

                using(SqlCommand command = new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Department Get(int id)
        {
            using (SqlConnection connection=new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"Select Id,Name from Departments where Id=@Id and IsActive=1";

                using(SqlCommand  cmd=new SqlCommand(query,connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                   using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(!reader.Read())
                            return null;

                        Department department = new Department();

                        department.Id=reader.GetInt32(reader.GetOrdinal("Id"));
                        department.Name = reader.GetString(reader.GetOrdinal("Name"));
                        

                        return department;
                    }
                         
                }
                
            }
        }

        public List<Department> GetAll()
        {
            using(SqlConnection connection=new SqlConnection(_connectionString))
            {
                connection.Open();

                List<Department> departments = new List<Department>();

                string query = @"Select Id,Name from Departments where IsActive=1";

                using (SqlCommand cmd=new SqlCommand(query,connection))
                {
                    SqlDataReader Reader = cmd.ExecuteReader();

                    while (Reader.Read())
                    {
                        Department department = new Department();
                        department.Id = (int)Reader["Id"];
                        department.Name = (string)Reader["Name"];
                        

                        departments.Add(department);
                        

                    }
                }

                return departments;
            }
        }

        
    }
}
