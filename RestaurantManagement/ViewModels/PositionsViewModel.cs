using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RestaurantManagement.Commands.Positions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.ViewModels
{
    public class PositionsViewModel:BaseViewModel
    {
        private readonly IUnitOfWork _db;

        public PositionsViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentPosition = new PositionsModel();
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
        private PositionsModel _currentPosition;
        public PositionsModel CurrentPosition
        {
            get => _currentPosition;
            set
            {
                _currentPosition = value;
                OnPropertyChanged(nameof(CurrentPosition));
            }
        }
        private PositionsModel _selectedPosition;
        public PositionsModel SelectedPosition
        {
            get => _selectedPosition;

            set
            {
                _selectedPosition = value;
                if (_selectedPosition != null)
                {
                    CurrentPosition = SelectedPosition.Clone();
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentPosition = new PositionsModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedPosition));
            }
        }

        public ObservableCollection<PositionsModel> Positions { get; set; }
        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(_db, this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
    }
}
