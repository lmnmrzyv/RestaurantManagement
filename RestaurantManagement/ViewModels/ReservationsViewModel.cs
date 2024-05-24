using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands.Reservations;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.ViewModels
{
    public class ReservationsViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _db;
        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<Table> Tables { get; set; }

        public ReservationsViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentReservation = new ReservationsModel();
            CurrentReservation.Table=new Table();
            CurrentReservation.Customer=new Customer();
            LoadCustomers();
            LoadTables();
        }

        private void LoadCustomers()
        {
            var customers = _db.CustomerRepository.GetAll();
            Customers = new ObservableCollection<Customer>(customers);
        }

        private void LoadTables()
        {
            var tables = _db.TableRepository.GetAll();
            Tables = new ObservableCollection<Table>(tables);
        }

        private State _state;
        public State CurrentState
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(CurrentState));
            }
        }

        private ReservationsModel _currentReservation;
        public ReservationsModel CurrentReservation
        {
            get => _currentReservation;
            set
            {
                _currentReservation = value;
                OnPropertyChanged(nameof(CurrentReservation));
            }
        }

        private ReservationsModel _selectedReservation;
        public ReservationsModel SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                _selectedReservation = value;
                if (_selectedReservation != null)
                {
                    CurrentReservation.Customer= _selectedReservation.Customer;
                    CurrentReservation.Table= _selectedReservation.Table;
                    CurrentReservation.No= _selectedReservation.No;
                    CurrentReservation.ReservationDate= _selectedReservation.ReservationDate;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentReservation = new ReservationsModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }

        public ObservableCollection<ReservationsModel> Reservations { get; set; }
        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(_db, this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
    }
}
