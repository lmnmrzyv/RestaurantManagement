using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Commands.Categories;

namespace RestaurantManagement.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _db;
        public CategoriesViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentCategories = new CategoriesModel();

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
        private CategoriesModel _currentCategories;

        public CategoriesModel CurrentCategories
        {
            get => _currentCategories;
            set
            {
                _currentCategories = value;
                OnPropertyChanged(nameof(CurrentCategories));
            }
        }

        private CategoriesModel _selectedCategories;

        public CategoriesModel SelectedCategories
        {
            get => _selectedCategories;
            set
            {
                _selectedCategories = value;
                if (_selectedCategories != null)
                {
                    CurrentCategories.No = SelectedCategories.No;
                    CurrentCategories.name = SelectedCategories.name;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentCategories = new CategoriesModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedCategories));
            }
        }
        public ObservableCollection<CategoriesModel> Categories { get; set; }

        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
    }
}
