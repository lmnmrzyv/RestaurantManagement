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
    public class PositionViewModel:BaseViewModel
    {
        private readonly IUnitOfWork _db;

        public PositionViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentPosition = new PositionModel();


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
        private PositionModel _currentPosition;
        public PositionModel CurrentPosition
        {
            get => _currentPosition;
            set
            {
                _currentPosition = value;
                OnPropertyChanged(nameof(CurrentPosition));
            }
        }
        private PositionModel _selectedPosition;
        public PositionModel SelectedPosition
        {
            get => _selectedPosition;

            set
            {
                _selectedPosition = value;
                if (_selectedPosition != null)
                {
                    CurrentPosition.Name = SelectedPosition.Name;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentPosition = new PositionModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedPosition));
            }
        }

        public ObservableCollection<EmployeesModel> Employees { get; set; }
    }
}
