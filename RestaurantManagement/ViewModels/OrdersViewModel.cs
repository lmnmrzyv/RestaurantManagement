using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Commands.Orders;
using System.Windows.Input;
using RestaurantManagement.Views;
using RestaurantManagement.Views.Controls;

namespace RestaurantManagement.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _db;
        public OrdersViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentOrders = new OrdersModel();

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
        private OrdersModel _currentOrders;

        public OrdersModel CurrentOrders
        {
            get => _currentOrders;
            set
            {
                _currentOrders = value;
                OnPropertyChanged(nameof(CurrentOrders));
            }
        }

        private OrdersModel _selectedOrders;

        public OrdersModel SelectedOrders
        {
            get => _selectedOrders;
            set
            {
                _selectedOrders = value;
                if (_selectedOrders != null)
                {
                    CurrentOrders.No = SelectedOrders.No;
                    CurrentOrders.OrderTime = SelectedOrders.OrderTime;
                    CurrentOrders.TotalPrice = SelectedOrders.TotalPrice;
                    CurrentOrders.PaymentMethod = SelectedOrders.PaymentMethod;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentOrders = new OrdersModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedOrders));
            }
        }
        private void ExecuteSeeOrderCommand(OrdersModel order)
        {
            if (order != null)
            {
                OrderDetailWindow orderDetailWindow = new OrderDetailWindow(order);
                orderDetailWindow.Show();
            }
        }

        public ICommand SeeOrderCommand { get; }
        public ObservableCollection<OrdersModel> Orders { get; set; }

        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
    }

}
