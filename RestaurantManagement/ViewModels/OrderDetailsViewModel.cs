using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.Commands.OrderDetails;
using DataAccessManager.Domain.Entities;

namespace RestaurantManagement.ViewModels
{
    public class OrderDetailsViewModel: BaseViewModel
    {
        private readonly IUnitOfWork _db;
        public ObservableCollection<MenuItem> MenuItems { get; set; }
        public OrderDetailsViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentOrderDetails = new OrderDetailsModel();
            CurrentOrderDetails.MenuItem = new MenuItem();
            LoadMenuItems();
        }
        private void LoadMenuItems()
        {
            var menuItems = _db.MenuItemRepository.GetAll();

            MenuItems = new ObservableCollection<MenuItem>(menuItems);
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
        private OrderDetailsModel _currentOrderDetails;

        public OrderDetailsModel CurrentOrderDetails
        {
            get => _currentOrderDetails;
            set
            {
                _currentOrderDetails = value;
                OnPropertyChanged(nameof(CurrentOrderDetails));
            }
        }

        private OrderDetailsModel _selectedOrderDetails;

        public OrderDetailsModel SelectedOrderDetails
        {
            get => _selectedOrderDetails;
            set
            {
                _selectedOrderDetails = value;
                if (_selectedOrderDetails != null)
                {
                    CurrentOrderDetails = SelectedOrderDetails.Clone();
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentOrderDetails = new OrderDetailsModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedOrderDetails));
            }
        }

        public ObservableCollection<OrderDetailsModel> OrderDetails { get; set; }

        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(_db, this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
    }
}
