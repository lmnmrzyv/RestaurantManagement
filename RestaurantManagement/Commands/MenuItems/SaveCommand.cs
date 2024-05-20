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

namespace RestaurantManagement.Commands.MenuItems
{
    public class SaveCommand : ICommand
    {
        private readonly MenuItemsViewModel _currentState;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, MenuItemsViewModel currentState)
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
            var mapper = new MenuItemMapper();
            var menuItem = mapper.MapModelToEntity(new MenuItem(), _currentState.CurrentMenuItems);

            _db.MenuItemRepository.Add(menuItem);

            var lastElementNo = _currentState.MenuItems.LastOrDefault()?.No ?? 0;

            _currentState.CurrentMenuItems.No = lastElementNo + 1;

            _currentState.MenuItems.Add(_currentState.CurrentMenuItems);
            _currentState.CurrentMenuItems = new MenuItemsModel();

            _currentState.CurrentState = State.NORMAL;
        }
    }
}
