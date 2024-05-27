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
            CurrentCustomers = new CustomerModel();

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
        private CustomerModel _currentCustomers;

        public CustomerModel CurrentCustomers
        {
            get => _currentCustomers;
            set
            {
                _currentCustomers = value;
                OnPropertyChanged(nameof(CurrentCustomers));
            }
        }

        private CustomerModel _selectedCustomers;

        public CustomerModel SelectedCustomers
        {
            get => _selectedCustomers;
            set
            {
                _selectedCustomers = value;
                if (_selectedCustomers != null)
                {
                    var CustomersTemp=new CustomerModel();
                    CustomersTemp.Id = _selectedCustomers.Id;
                    CustomersTemp.No = SelectedCustomers.No;
                    CustomersTemp.Name = SelectedCustomers.Name;
                    CustomersTemp.Surname = SelectedCustomers.Surname;
                    CustomersTemp.PhoneNum = SelectedCustomers.PhoneNum;
                    CustomersTemp.Mail = SelectedCustomers.Mail;
                    CurrentCustomers=CustomersTemp;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentCustomers = new CustomerModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedCustomers));
            }
        }

        public List<CustomerModel> AllCustomers { get; set; }
        private ObservableCollection<CustomerModel> _customers;
        public ObservableCollection<CustomerModel> Customers
        {
            get=> _customers;
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }
        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
    }
}
