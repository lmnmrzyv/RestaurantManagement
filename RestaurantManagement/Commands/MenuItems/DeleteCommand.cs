using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.MenuItems
{
    public class DeleteCommand : ICommand
    {
        private readonly IUnitOfWork _db;
        private readonly MenuItemsViewModel _viewModel;
        public DeleteCommand(IUnitOfWork db, MenuItemsViewModel viewModel)
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
            var result = MessageBox.Show("Are you sure you want to delete selected item?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            var deletedId = _viewModel.SelectedMenuItem.Id;

            _db.MenuItemRepository.Delete(deletedId);

            MessageBox.Show("Successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _viewModel.MenuItems.Remove(_viewModel.SelectedMenuItem);
            _viewModel.AllMenuItems = _viewModel.MenuItems.ToList();

            for (int i = 0; i < _viewModel.MenuItems.Count; i++)
            {
                _viewModel.MenuItems[i].No = i + 1;
            }
            _viewModel.MenuItems = new ObservableCollection<MenuItemsModel>(_viewModel.MenuItems);
            _viewModel.CurrentState = State.NORMAL;
        }
    }
}
