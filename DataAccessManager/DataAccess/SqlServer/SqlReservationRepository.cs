using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessManager.DataAccess.SqlServer
{
    public class SqlReservationRepository : IReservationRepository
    {
        private readonly string _connectionString;
        public SqlReservationRepository(string connectionString) 
        {
            connectionString=_connectionString;
        }
        public int Add(Reservation item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Reservations (ReservationDate,NumberOfPeople,TableId,CustomerId,IsActive) output inserted.Id VALUES 
                 (@ReservationDate,@NumberOfPeople, @TableId,@CustomerId,1)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationDate", item.ReservationDate);
                    command.Parameters.AddWithValue("@NumberOfPeople", item.NumberOfPeople);
                    command.Parameters.AddWithValue("@TableId", item.TableId);
                    command.Parameters.AddWithValue("@CustomerId", item.CustomerId);
                   // command.Parameters.AddWithValue("@IsActive", item.IsActive);
                    

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
                    command.Parameters.AddWithValue("@TableId", item.TableId);
                    command.Parameters.AddWithValue("@CustomerId", item.CustomerId);
  
                    command.ExecuteNonQuery();
                }

            }
        }

        public void Delete(Reservation item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"UPDATE Reservations SET IsActive=0 where Id=@Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.ExecuteNonQuery();
                }

            }
        }

        public Reservation Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"Select Id,ReservationDate,NumberOfPeople,TableId,CustomerId from Reservations where Id=@Id and IsActive=1";

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
                        reservation.TableId = reader.GetInt32(reader.GetOrdinal("TableId"));
                        reservation.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));
                        

                        return reservation;
                    }

                }

            }
        }

        public List<Reservation> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                List<Reservation> reservations = new List<Reservation>();

                string query = "Select Id,ReservationDate,NumberOfPeople,TableId,CustomerId from Reservations where  IsActive=1";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataReader Reader = cmd.ExecuteReader();

                    while (Reader.Read())
                    {
                        Reservation reservation = new Reservation();
                        reservation.Id = (int)Reader["Id"];
                        reservation.ReservationDate = (DateTime)Reader["ReservationDate"];
                        reservation.NumberOfPeople = (int)Reader["NumberOfPeople"];
                        reservation.TableId = (int)Reader["TableId"];
                        reservation.CustomerId = (int)Reader["CustomerId"];
                        


                        reservations.Add(reservation);


                    }
                }

                return reservations;
            }
        }

        
    }
}
