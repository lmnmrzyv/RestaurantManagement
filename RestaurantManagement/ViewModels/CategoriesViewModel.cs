using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands.Categories;
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
    public class CategoriesViewModel : BaseViewModel, IControl
    {

        private readonly IUnitOfWork _db;
        public CategoriesViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentCategory = new CategoriesModel();
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
        private CategoriesModel _currentCategory;
        public CategoriesModel CurrentCategory
        {
            get => _currentCategory;
            set
            {
                _currentCategory = value;
                OnPropertyChanged(nameof(CurrentCategory));
            }
        }
        private CategoriesModel _selectedCategory;
        public CategoriesModel SelectedCategory
        {
            get => _selectedCategory;

            set
            {
                _selectedCategory = value;
                if (_selectedCategory != null)
                {
                    var CategoriesTemp = new CategoriesModel();
                    CategoriesTemp.name = SelectedCategory.name;
                    CategoriesTemp.Id = SelectedCategory.Id;
                    CategoriesTemp.No = SelectedCategory.No;
                    CurrentCategory = CategoriesTemp;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentCategory = new CategoriesModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedCategory));

            }
        }
        private ObservableCollection<CategoriesModel> _categories { get; set; }
        public ObservableCollection<CategoriesModel> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }


        public List<CategoriesModel> AllCategories { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                var lowerSearchText = SearchText.ToLower();

                var filteredCategories = AllCategories.Where(x => x.name.ToLower().Contains(lowerSearchText));

                Categories = new ObservableCollection<CategoriesModel>(filteredCategories);
            }
        }
        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(_db, this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);

        public string Header => "Categories";
    }
}
