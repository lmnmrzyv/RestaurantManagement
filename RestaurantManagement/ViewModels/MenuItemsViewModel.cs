using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.EnumsDB;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands.MenuItems;
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

namespace RestaurantManagement.ViewModels
{
    public class MenuItemsViewModel : BaseViewModel, IControl
    {

        private readonly IUnitOfWork _db;
        public ObservableCollection<CategoriesModel> Categories { get; set; }
        public MenuItemsViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentMenuItem = new MenuItemsModel();
        }
        public void LoadCategories()
        {
            var categories = _db.CategoryRepository.GetAll();

            Categories = new ObservableCollection<CategoriesModel>(categories.Select(c => new CategoriesModel
            {
                Id = c.Id,
                name = c.name
            }));
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
        private MenuItemsModel _currentMenuItem;
        public MenuItemsModel CurrentMenuItem
        {
            get => _currentMenuItem;
            set
            {
                _currentMenuItem = value;
                OnPropertyChanged(nameof(CurrentMenuItem));
            }
        }
        private MenuItemsModel _selectedMenuItem;
        public MenuItemsModel SelectedMenuItem
        {
            get => _selectedMenuItem;

            set
            {
                _selectedMenuItem = value;
                if (_selectedMenuItem != null)
                {
                    CloneRef<MenuItemsModel> cloner = new CloneRef<MenuItemsModel>();
                    var MenuItemTmp = cloner.Clone(_selectedMenuItem);
                    CurrentMenuItem = MenuItemTmp;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentMenuItem = new MenuItemsModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedMenuItem));

            }
        }
        private ObservableCollection<MenuItemsModel> _menuItem { get; set; }
        public ObservableCollection<MenuItemsModel> MenuItems
        {
            get => _menuItem;
            set
            {
                _menuItem = value;
                OnPropertyChanged(nameof(MenuItems));
            }
        }


        public List<MenuItemsModel> AllMenuItems { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                var lowerSearchText = SearchText.ToLower();

                var filteredMenuItems = AllMenuItems.Where(x => x.name.ToLower().Contains(lowerSearchText));

                MenuItems = new ObservableCollection<MenuItemsModel>(filteredMenuItems);
            }
        }
        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(_db, this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);

        public string Header => "MenuItems";
    }
}
