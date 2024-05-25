using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using DataAccessManager.DataAccess.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlReservationRepository : IReservationRepository
    {
        private readonly string _connectionString;
        public SqlReservationRepository(string connectionString) 
        {
            _connectionString=connectionString;
        }
        public int Add(Reservation item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Reservations (ReservationDate, NumberOfPeople, TableId, CustomerId, IsActive) 
                         OUTPUT inserted.Id 
                         VALUES (@ReservationDate, @NumberOfPeople, @TableId, @CustomerId, 1)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationDate", item.ReservationDate);
                    command.Parameters.AddWithValue("@NumberOfPeople", item.NumberOfPeople);
                    command.Parameters.AddWithValue("@TableId", item.Table.Id);
                    command.Parameters.AddWithValue("@CustomerId", item.Customer.Id);

                    return (int)command.ExecuteScalar();
                }
            }
        }

        public void Update(Reservation item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"UPDATE Reservations SET ReservationDate=@ReservationDate,NumberOfPeople=@NumberOfPeople
                , TableId=@TableId,CustomerId=@CustomerId where Id=@Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@ReservationDate", item.ReservationDate);
                    command.Parameters.AddWithValue("@NumberOfPeople", item.NumberOfPeople);
                    command.Parameters.AddWithValue("@TableId", item.Table.Id);
                    command.Parameters.AddWithValue("@CustomerId", item.Customer.Id);

                    command.ExecuteNonQuery();
                }

            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"Update  Reservations SET IsActive=0 where Id=@Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }

            }
        }

        public Reservation Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"SELECT r.Id, r.ReservationDate, r.NumberOfPeople, r.TableId, r.CustomerId, r.IsActive, 
                                t.Id, t.TableNumber, c.Id, c.Name, c.Surname
                        FROM Reservations r
                        INNER JOIN Tables t ON r.TableId = t.Id
                        INNER JOIN Customers c ON r.CustomerId = c.Id
                        WHERE r.Id = @Id AND r.IsActive = 1";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        Reservation reservation = new Reservation();
                        reservation.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        reservation.ReservationDate = reader.GetDateTime(reader.GetOrdinal("ReservationDate"));
                        reservation.NumberOfPeople = reader.GetInt32(reader.GetOrdinal("NumberOfPeople"));
                        reservation.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));

                        // Table ve Customer nesnelerini oluşturup verilerini doldurun
                        Table table = new Table();
                        table.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        table.TableNumber = reader.GetInt32(reader.GetOrdinal("TableNumber")); // Varsayılan olarak sütun adını kullanarak değeri alıyorum, uygun şekilde değiştirebilirsiniz
      

                        Customer customer = new Customer();
                        customer.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        customer.Name = reader.GetString(reader.GetOrdinal("Name")); // Varsayılan olarak sütun adını kullanarak değeri alıyorum, uygun şekilde değiştirebilirsiniz
                        customer.Surname = reader.GetString(reader.GetOrdinal("Surname")); // Varsayılan olarak sütun adını kullanarak değeri alıyorum, uygun şekilde değiştirebilirsiniz

                        // Reservation nesnesine Table ve Customer nesnelerini ekleyin
                        reservation.Table = table;
                        reservation.Customer = customer;

                        return reservation;
                    }
                }
            }
        }


        public List<Reservation> GetAll()
        {
            List<Reservation> reservations = new List<Reservation>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"SELECT r.Id, r.ReservationDate, r.NumberOfPeople, r.TableId, r.CustomerId, r.IsActive, 
                        t.Id , t.TableNumber, 
                        c.Id , c.Name, c.Surname
                        FROM Reservations r
                        INNER JOIN Tables t ON r.TableId = t.Id
                        INNER JOIN Customers c ON r.CustomerId = c.Id
                        WHERE r.IsActive = 1";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation();
                        reservation.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        reservation.ReservationDate = reader.GetDateTime(reader.GetOrdinal("ReservationDate"));
                        reservation.NumberOfPeople = reader.GetInt32(reader.GetOrdinal("NumberOfPeople"));
                        reservation.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));

                        // Table nesnesini oluşturup verilerini doldurun
                        Table table = new Table();
                        table.Id = reader.GetInt32(reader.GetOrdinal("TableId"));
                        table.TableNumber = reader.GetInt32(reader.GetOrdinal("TableNumber"));
                        reservation.Table = table;

                        // Customer nesnesini oluşturup verilerini doldurun
                        Customer customer = new Customer();
                        customer.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        customer.Name = reader.GetString(reader.GetOrdinal("Name"));
                        customer.Surname = reader.GetString(reader.GetOrdinal("Surname"));
                        reservation.Customer = customer;

                        reservations.Add(reservation);
                    }
                }
            }

            return reservations;
        }



    }
}
