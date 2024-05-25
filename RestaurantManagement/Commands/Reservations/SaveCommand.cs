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
        private readonly ReservationsViewModel _currentState;
        private readonly IUnitOfWork _db;

        public SaveCommand(IUnitOfWork db, ReservationsViewModel currentState)
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

            // Yeni bir departman mı oluşturuluyor yoksa mevcut bir departman mı güncelleniyor kontrol et
            if (reservation.Id == 0)
            {
                // Yeni departmanı veritabanına ekle ve dönen ID ile modeli güncelle
                _currentState.CurrentReservation.Id = _db.ReservationRepository.Add(reservation);

                // Departmanın yeni numarasını belirle
                var lastElementNo = _currentState.Reservations.LastOrDefault()?.No ?? 0;
                _currentState.CurrentReservation.No = lastElementNo + 1;

                // Mevcut departman modelini observable koleksiyona ekle
                _currentState.Reservations.Add(_currentState.CurrentReservation);

                MessageBox.Show("Başarıyla oluşturuldu", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Gerekirse mevcut departmanı veritabanından al
                var existingDepartment = _db.ReservationRepository.Get(reservation.Id);

                // Mevcut departmanı veritabanında güncelle
                _db.ReservationRepository.Update(reservation);

                // Observable koleksiyondaki departmanın indeksini bul
                var index = _currentState.Reservations.IndexOf(_currentState.Reservations.First(x => x.Id == reservation.Id));

                // Observable koleksiyondaki departman modelini güncelle
                _currentState.Reservations[index] = _currentState.CurrentReservation;

                MessageBox.Show("Başarıyla güncellendi", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Mevcut departman modelini sıfırla
            _currentState.CurrentReservation = new ReservationsModel();
            _currentState.SelectedReservation = null;
            _currentState.CurrentState = State.NORMAL;
        }
    }
}
