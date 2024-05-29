using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using RestaurantManagement.Views.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RestaurantManagement.Commands
{
    public class OpenMenuItemCommand : ICommand
    {
        private readonly IUnitOfWork _db;
        public OpenMenuItemCommand(IUnitOfWork db)
        {
            _db = db;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var grid = parameter as Grid;

            if (grid == null)
                return;

            grid.Children.Clear();

            var control = new MenuItemsControl();
            var viewModel = new MenuItemsViewModel(_db);
            viewModel.LoadCategories();

            var menuItems = _db.MenuItemRepository.GetAll();

            var menuItemsModel = new List<MenuItemsModel>();

            var menuItemsMapper = new MenuItemMapper();
            var no = 1;

            foreach (var menuItem in menuItems)
            {
                var menuItemModel = menuItemsMapper.MapEntityToModel(menuItem, new MenuItemsModel());

                menuItemModel.No = no++;

                menuItemsModel.Add(menuItemModel);
            }

            viewModel.AllMenuItems = menuItemsModel;
            viewModel.MenuItems = new ObservableCollection<MenuItemsModel>(menuItemsModel);

            control.DataContext = viewModel;

            grid.Children.Add(control);
        }
    }
}
