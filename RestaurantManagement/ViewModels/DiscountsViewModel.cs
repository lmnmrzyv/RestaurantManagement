using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands.Discounts;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.ViewModels
{
    public class DiscountsViewModel : BaseViewModel , IControl
    {
        private readonly IUnitOfWork _db;
        public ObservableCollection<CategoriesModel> Categories { get; set; }
        public DiscountsViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentDiscount = new DiscountModel();
            CurrentDiscount.Category = new CategoriesModel();

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
        private DiscountModel _currentDiscount;
        public DiscountModel CurrentDiscount
        {
            get => _currentDiscount;
            set
            {
                _currentDiscount = value;
                OnPropertyChanged(nameof(CurrentDiscount));
            }
        }
        private DiscountModel _selectedDiscount;

        public DiscountModel SelectedDiscount
        {
            get => _selectedDiscount;
            set
            {
                _selectedDiscount = value;
                if (_selectedDiscount != null)
                {
                    CloneRef<DiscountModel> cloner = new CloneRef<DiscountModel>();
                    var DiscountTemp = cloner.Clone(_selectedDiscount);
                    CurrentDiscount = DiscountTemp;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentDiscount = new DiscountModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedDiscount));
                 
            }
        }
        public ObservableCollection<DiscountModel> _discounts { get; set; }
        public ObservableCollection<DiscountModel> Discounts
        {
            get => _discounts;
            set
            {
                _discounts = value;
                OnPropertyChanged(nameof(Discounts));
            }
        }


        public List<DiscountModel> AllDiscounts { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                var lowerSearchText = SearchText.ToLower();

                var filteredDiscounts = AllDiscounts.Where(x => x.No==Convert.ToInt32((lowerSearchText)));

                Discounts = new ObservableCollection<DiscountModel>(filteredDiscounts);
            }
        }
        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(_db, this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
        
        public string Header => "Discounts";
    }
}

