using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands.Reservations;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.ViewModels
{
    public class ReservationsViewModel:BaseViewModel
    {
        private readonly IUnitOfWork _db;
        public ReservationsViewModel(IUnitOfWork db)
        {
            _db = db;
        }

        private State _state;
        public State CurrentState
        {
            get => _state;
            
            set
            {
                _state= value;
                OnPropertyChanged(nameof(CurrentState));
            }
        }
        private ReservationsModel _currentReservation;
        public ReservationsModel CurrentReservation
        {
            get=> _currentReservation;
            set
            {
                _currentReservation= value;
                OnPropertyChanged(nameof(CurrentReservation));
            }
        }

        private ReservationsModel _selectedReservation;
        public ReservationsModel SelectedReservation
        {
            get=>_selectedReservation;
            set
            {
                _selectedReservation= value;
                if(_selectedReservation!=null)
                {

                }
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }


        public List<ReservationsModel> Reservations { get; set; }
        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(this);
        public DeleteCommand Delete => new DeleteCommand(this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
    }
}
