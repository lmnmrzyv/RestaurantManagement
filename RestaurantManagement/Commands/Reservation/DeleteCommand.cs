using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Reservations
{
    public class DeleteCommand : ICommand
    {
        private readonly IUnitOfWork _db;
        private readonly ReservationsViewModel _viewModel;
        public DeleteCommand(IUnitOfWork db,ReservationsViewModel viewModel)
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
            var result = MessageBox.Show("Are you sure you want to delete selected item?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            var deletedId = _viewModel.SelectedReservation.Id;

            _db.ReservationRepository.Delete(deletedId);

            MessageBox.Show("Successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _viewModel.Reservations.Remove(_viewModel.SelectedReservation);
            _viewModel.AllReservations = _viewModel.Reservations.ToList();

            for (int i = 0; i < _viewModel.Reservations.Count; i++)
            {
                _viewModel.Reservations[i].No = i + 1;
            }
            _viewModel.Reservations = new ObservableCollection<ReservationModel>(_viewModel.Reservations);
            _viewModel.CurrentState = State.NORMAL;
        }
    }
}
