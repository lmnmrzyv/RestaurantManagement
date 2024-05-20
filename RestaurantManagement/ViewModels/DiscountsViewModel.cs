using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.ViewModels
{
    public class DiscountsViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _db;
        public DiscountsViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentDiscounts = new DiscountsModel();

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
        private DiscountsModel _currentDiscounts;

        public DiscountsModel CurrentDiscounts
        {
            get => _currentDiscounts;
            set
            {
                _currentDiscounts = value;
                OnPropertyChanged(nameof(CurrentDiscounts));
            }
        }

        private DiscountsModel _selectedDiscounts;

        public DiscountsModel SelectedDiscounts
        {
            get => _selectedDiscounts;
            set
            {
                _selectedDiscounts = value;
                if (_selectedDiscounts != null)
                {
                    CurrentDiscounts.No = SelectedDiscounts.No;
                    CurrentDiscounts.startTime = SelectedDiscounts.startTime;
                    CurrentDiscounts.endTime = SelectedDiscounts.endTime;
                    CurrentDiscounts.Status = SelectedDiscounts.Status;
                    CurrentDiscounts.CategoryId = SelectedDiscounts.CategoryId;
                    CurrentDiscounts.Percent = SelectedDiscounts.Percent;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentDiscounts = new DiscountsModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedDiscounts));
            }
        }
        public ObservableCollection<DiscountsModel> Discounts { get; set; }

        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
    }
}
}
