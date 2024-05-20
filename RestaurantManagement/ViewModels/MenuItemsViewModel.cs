using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Commands.MenuItems;

namespace RestaurantManagement.ViewModels
{
    public class MenuItemsViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _db;
        public MenuItemsViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentMenuItems = new MenuItemsModel();

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
        private MenuItemsModel _currentMenuItems;

        public MenuItemsModel CurrentMenuItems
        {
            get => _currentMenuItems;
            set
            {
                _currentMenuItems = value;
                OnPropertyChanged(nameof(CurrentMenuItems));
            }
        }

        private MenuItemsModel _selectedMenuItems;

        public MenuItemsModel SelectedMenuItems
        {
            get => _selectedMenuItems;
            set
            {
                _selectedMenuItems = value;
                if (_selectedMenuItems != null)
                {
                    CurrentMenuItems.No = SelectedMenuItems.No;
                    CurrentMenuItems.name = SelectedMenuItems.name;
                    CurrentMenuItems.description = SelectedMenuItems.description;
                    CurrentMenuItems.price = SelectedMenuItems.price;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentMenuItems = new MenuItemsModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedMenuItems));
            }
        }
        public ObservableCollection<MenuItemsModel> MenuItems { get; set; }

        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
    }
}
