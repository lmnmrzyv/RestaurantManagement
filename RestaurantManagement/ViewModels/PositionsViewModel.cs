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
using RestaurantManagement.ViewModels.Interfaces;
using DataAccessManager.Domain.Entities;
using System.Xml.Linq;

namespace RestaurantManagement.ViewModels
{
    public class PositionsViewModel:BaseViewModel, IControl
    {
        private readonly IUnitOfWork _db;
        public ObservableCollection<DepartmentModel> Departments { get; set; }
        public PositionsViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentPosition = new PositionsModel();
        }
        public void LoadDepartments()
        {
            var departments = _db.DepartmentRepository.GetAll();

            Departments = new ObservableCollection<DepartmentModel>(departments.Select(d => new DepartmentModel
            {
                Id = d.Id,
                Name = d.Name
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
                    CloneRef<PositionsModel> cloner = new CloneRef<PositionsModel>();
                    var Positiontmp = cloner.Clone(_selectedPosition);
                    CurrentPosition = Positiontmp;
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
        private ObservableCollection<PositionsModel> _positions { get; set; }
        public ObservableCollection<PositionsModel> Positions
        {
            get => _positions;
            set
            {
                _positions = value;
                OnPropertyChanged(nameof(Positions));
            }
        }
        public List<PositionsModel> AllPositions { get; set; }      
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                var lowerSearchText = SearchText.ToLower();

                var filteredPositions = AllPositions.Where(x => x.Name.ToLower().Contains(lowerSearchText));
                Positions = new ObservableCollection<PositionsModel>(filteredPositions);
            }
        }
        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(_db, this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);

        public string Header => "Positions";

    }
}
