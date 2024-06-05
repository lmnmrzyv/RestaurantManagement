using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands.Orders;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.ViewModels
{
    public class OrdersViewModel : BaseViewModel, IControl
    {

        private readonly IUnitOfWork _db;
        public OrdersViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentOrder = new OrdersModel();
            SeeDetailsCommand = new SeeDetailsCommand(this);

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
        private OrdersModel _currentOrder;
        public OrdersModel CurrentOrder
        {
            get => _currentOrder;
            set
            {
                _currentOrder = value;
                OnPropertyChanged(nameof(CurrentOrder));
            }
        }
        private OrdersModel _selectedOrder;
        public OrdersModel SelectedOrder
        {
            get => _selectedOrder;

            set
            {
                _selectedOrder = value;
                if (_selectedOrder != null)
                {
                    CloneRef<OrdersModel> cloner = new CloneRef<OrdersModel>();
                    var OrderTmp = cloner.Clone(_selectedOrder);
                    CurrentOrder = OrderTmp;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentOrder = new OrdersModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedOrder));

            }
        }
        private ObservableCollection<OrdersModel> _orders { get; set; }
        public ObservableCollection<OrdersModel> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }


        public List<OrdersModel> AllOrders { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                var lowerSearchText = SearchText.ToLower();

                var filteredOrders = AllOrders.Where(x => x.PaymentMethod.ToLower().Contains(lowerSearchText));
  
                  Orders = new ObservableCollection<OrdersModel>(filteredOrders);
            }
        }
        public ICommand SeeDetailsCommand { get; }

        public string Header => "Orders";

        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(_db, this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);

        //public SeeDetailsCommand SeeDetails => new SeeDetailsCommand(this);

    }
}
