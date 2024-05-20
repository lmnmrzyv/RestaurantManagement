using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using RestaurantManagement.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Discounts
{
    public class SaveCommand : ICommand
    {
        private readonly DiscountsViewModel _currentState;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, DiscountsViewModel currentState)
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
            var mapper = new DiscountMapper();
            var discounts = mapper.MapModelToEntity(new Discount(), _currentState.CurrentDiscounts);

            _db.DiscountRepository.Add(discounts);

            var lastElementNo = _currentState.Discounts.LastOrDefault()?.No ?? 0;

            _currentState.CurrentDiscounts.No = lastElementNo + 1;

            _currentState.Discounts.Add(_currentState.CurrentDiscounts);
            _currentState.CurrentDiscounts = new DiscountsModel();

            _currentState.CurrentState = State.NORMAL;
        }
    }
}
