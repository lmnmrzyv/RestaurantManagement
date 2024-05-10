using DataAccessManager.Domain.Entities;
using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Mappers
{
    public class ReservationMapper : IMapper<ReservationsModel, Reservation>
    {
        public ReservationsModel Map(Reservation entity)
        {
            var reservationModel=new ReservationsModel();
            reservationModel.Table = entity.Table;
            reservationModel.ReservationDate = entity.ReservationDate;
            reservationModel.NumberOfPeople = entity.NumberOfPeople;
            reservationModel.Customer= entity.Customer;

            return reservationModel;
        }

        public Reservation Map(ReservationsModel model)
        {
            var reservation = new Reservation();
            reservation.Table = model.Table;
            reservation.ReservationDate = model.ReservationDate;
            reservation.NumberOfPeople = model.NumberOfPeople;
            reservation.Customer = model.Customer;

            return reservation;
        }
    }
}
