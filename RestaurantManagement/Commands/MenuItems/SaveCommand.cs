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
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.MenuItems
{
    public class SaveCommand : ICommand
    {
        private readonly MenuItemsViewModel _viewModel;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, MenuItemsViewModel viewModel)
        {
            _db = db;
            _viewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var mapper = new MenuItemMapper();
            var menuItem = mapper.MapModelToEntity(new MenuItem(), _viewModel.CurrentMenuItem);
            if (menuItem.Id == 0)
            {
                _viewModel.CurrentMenuItem.Id = _db.MenuItemRepository.Add(menuItem);
                var lastElementNo = _viewModel.MenuItems.LastOrDefault()?.No ?? 0;
                _viewModel.CurrentMenuItem.No = lastElementNo + 1;
                _viewModel.MenuItems.Add(_viewModel.CurrentMenuItem);
                _viewModel.AllMenuItems = _viewModel.MenuItems.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var existingMenuItem = _db.MenuItemRepository.Get(menuItem.Id);
                _db.MenuItemRepository.Update(menuItem);
                var index = _viewModel.MenuItems.IndexOf(_viewModel.MenuItems.First(x => x.Id == menuItem.Id));
                _viewModel.MenuItems[index] = _viewModel.CurrentMenuItem;
                _viewModel.AllMenuItems = _viewModel.MenuItems.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            _viewModel.CurrentMenuItem = new MenuItemsModel();
            _viewModel.SelectedMenuItem = null;
            _viewModel.CurrentState = State.NORMAL;
        }

    }
}
