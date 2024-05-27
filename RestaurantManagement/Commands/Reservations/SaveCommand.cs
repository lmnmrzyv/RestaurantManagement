using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Reservations
{
    public class SaveCommand : ICommand
    {
        private readonly ReservationsViewModel _viewModel;
        private readonly IUnitOfWork _db;

        public SaveCommand(IUnitOfWork db, ReservationsViewModel viewModel)
        {
            _db = db;
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var mapper = new ReservationMapper();
            var reservation = mapper.MapModelToEntity(new Reservation(), _viewModel.CurrentReservation);

            if (reservation.Id == 0)
            {
                _viewModel.CurrentReservation.Id = _db.ReservationRepository.Add(reservation);

                var lastElementNo = _viewModel.Reservations.LastOrDefault()?.No ?? 0;
                _viewModel.CurrentReservation.No = lastElementNo + 1;

                
                _viewModel.Reservations.Add(_viewModel.CurrentReservation);
                _viewModel.AllReservations = _viewModel.Reservations.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var existingDepartment = _db.ReservationRepository.Get(reservation.Id);

                _db.ReservationRepository.Update(reservation);

                var index = _viewModel.Reservations.IndexOf(_viewModel.Reservations.First(x => x.Id == reservation.Id));

                _viewModel.Reservations[index] = _viewModel.CurrentReservation;
                _viewModel.AllReservations = _viewModel.Reservations.ToList();

                MessageBox.Show("Successfully updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

             _viewModel.CurrentReservation = new ReservationModel();
             _viewModel.SelectedReservation = null;
            _viewModel.CurrentState = State.NORMAL;
        }
    }
}
