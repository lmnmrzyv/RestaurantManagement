using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
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
        private readonly ReservationsViewModel _currentState;
        public DeleteCommand(IUnitOfWork db,ReservationsViewModel currentState)
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
            var result = MessageBox.Show("Are you sure you want to delete selected item?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            var deletedId = _currentState.SelectedReservation.Id;

            _db.DepartmentRepository.Delete(deletedId);

            MessageBox.Show("Successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _currentState.Reservations.Remove(_currentState.SelectedReservation);

            for (int i = 0; i < _currentState.Reservations.Count; i++)
            {
                _currentState.Reservations[i].No = i + 1;
            }
            _currentState.CurrentState = State.NORMAL;
        }
    }
}
