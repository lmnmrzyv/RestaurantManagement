using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands.Customers;
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
    public class CustomersViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _db;
        public CustomersViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentCustomers = new CustomersModel();

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
        private CustomersModel _currentCustomers;

        public CustomersModel CurrentCustomers
        {
            get => _currentCustomers;
            set
            {
                _currentCustomers = value;
                OnPropertyChanged(nameof(CurrentCustomers));
            }
        }

        private CustomersModel _selectedCustomers;

        public CustomersModel SelectedCustomers
        {
            get => _selectedCustomers;
            set
            {
                _selectedCustomers = value;
                if (_selectedCustomers != null)
                {
                    CurrentCustomers.No = SelectedCustomers.No;
                    CurrentCustomers.Name = SelectedCustomers.Name;
                    CurrentCustomers.Surname = SelectedCustomers.Surname;
                    CurrentCustomers.PhoneNum = SelectedCustomers.PhoneNum;
                    CurrentCustomers.Mail = SelectedCustomers.Mail;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentCustomers = new CustomersModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedCustomers));
            }
        }
        public ObservableCollection<CustomersModel> Customers { get; set; }

        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
    }
}
