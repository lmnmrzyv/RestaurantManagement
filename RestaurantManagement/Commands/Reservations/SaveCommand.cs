using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Reservations
{
    public class SaveCommand : ICommand
    {
        private readonly IUnitOfWork _db;
        private readonly ReservationsViewModel _currentState;
        public SaveCommand(IUnitOfWork db,ReservationsViewModel currentState)
        {
            _db = db;
            _currentState = currentState;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var mapper = new ReservationMapper();
            var reservation = mapper.MapModelToEntity(new Reservation(), _currentState.CurrentReservation);
            _db.ReservationRepository.Add(reservation);

            var lastElementNo=_currentState.Reservations.LastOrDefault()?.No ?? 0;
            _currentState.CurrentReservation.No= lastElementNo+1;
            _currentState.Reservations.Add(_currentState.CurrentReservation);
            _currentState.CurrentReservation = new ReservationsModel();
            _currentState.CurrentState = State.NORMAL;
        }
    }
}
