using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands.Customers;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.ViewModels
{
    public class CustomersViewModel : BaseViewModel , IControl
    {
        private readonly IUnitOfWork _db;
        public CustomersViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentCustomer = new CustomerModel();

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
        private CustomerModel _currentCustomer;
        public CustomerModel CurrentCustomer
        {
            get => _currentCustomer;
            set
            {
                _currentCustomer = value;
                OnPropertyChanged(nameof(CurrentCustomer));
            }
        }

        private CustomerModel _selectedCustomer;

        public CustomerModel SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                if (_selectedCustomer != null)
                {
                    CloneRef<CustomerModel> cloner = new CloneRef<CustomerModel>();
                    var CustomerTemp =cloner.Clone(_selectedCustomer);
                    CurrentCustomer = CustomerTemp;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentCustomer = new CustomerModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }
        public List<CustomerModel> AllCustomers { get; set; }
        private ObservableCollection<CustomerModel> _customers { get; set; }
        public ObservableCollection<CustomerModel> Customers
        {
            get=> _customers;
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                var lowerSearchText = SearchText.ToLower();

                var filteredCustomers = AllCustomers.Where(x => x.Name.ToLower().Contains(lowerSearchText));

                Customers = new ObservableCollection<CustomerModel>(filteredCustomers);
            }
        }
        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(_db, this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
        public string Header => "Customers";

    }
}
