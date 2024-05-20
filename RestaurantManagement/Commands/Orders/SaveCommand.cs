using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Orders
{
    public class SaveCommand : ICommand
    {
        private readonly OrdersViewModel _currentState;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, OrdersViewModel currentState)
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
            var mapper = new OrderMapper();
            var orders = mapper.MapModelToEntity(new Order(), _currentState.CurrentOrders);

            _db.OrderRepository.Add(orders);

            var lastElementNo = _currentState.Orders.LastOrDefault()?.No ?? 0;

            _currentState.CurrentOrders.No = lastElementNo + 1;

            _currentState.Orders.Add(_currentState.CurrentOrders);
            _currentState.CurrentOrders = new OrdersModel();

            _currentState.CurrentState = State.NORMAL;
        }
    }
}
