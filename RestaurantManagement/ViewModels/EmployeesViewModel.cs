using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.EnumsDB;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands.Employees;
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
    public class EmployeesViewModel:BaseViewModel,IControl
    {

        private readonly IUnitOfWork _db;
        public ObservableCollection<Position> Positions { get; set; }
        public EmployeesViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentEmployees=new EmployeesModel();
            CurrentEmployees.Position = new Position();

            LoadPositions();
        }

        private void LoadPositions()
        {
            var positions = _db.PositionRepository.GetAll();

            Positions = new ObservableCollection<Position>(positions);
        }
        private State _state;

        public State CurrentState
        {
            get=> _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(CurrentState));
            }
        }
        private EmployeesModel _currentEmployees;
        public EmployeesModel CurrentEmployees
        {
            get => _currentEmployees;
            set
            {
                _currentEmployees = value;
                OnPropertyChanged(nameof(CurrentEmployees));
            }
        }
        private EmployeesModel _selectedEmployees;
        public EmployeesModel SelectedEmployees
        {
            get => _selectedEmployees;

            set
            {
               _selectedEmployees= value;
                if(_selectedEmployees!=null)
                {
                    CurrentEmployees.EducationLevel = SelectedEmployees.EducationLevel;
                    CurrentEmployees.PerformanceRating = SelectedEmployees.PerformanceRating;
                    CurrentEmployees.Surname= SelectedEmployees.Surname;
                    CurrentEmployees.Name= SelectedEmployees.Name;
                    CurrentEmployees.Id=SelectedEmployees.Id;
                    CurrentEmployees.No=SelectedEmployees.No;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentEmployees = new EmployeesModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedEmployees));
                OnPropertyChanged(nameof(CurrentEmployees));
            }
        }
        private ObservableCollection<EmployeesModel> _employees { get; set; }
        public ObservableCollection<EmployeesModel> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        public List<EducationLevel> EducationLevels => Enum.GetValues(typeof(EducationLevel)).Cast<EducationLevel>().ToList();

        public List<EmployeesModel> AllEmployees { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                var lowerSearchText = SearchText.ToLower();

                var filteredEmployees = AllEmployees.Where(x => x.Name.ToLower().Contains(lowerSearchText) ||
                                                       (x.Surname.ToLower().Contains(lowerSearchText)) || (x.Position.ToString().ToLower().Contains(lowerSearchText)) || (x.EducationLevel.ToString().ToLower().Contains(lowerSearchText)) || (x.PerformanceRating.ToString().ToLower().Contains(lowerSearchText)));

                Employees = new ObservableCollection<EmployeesModel>(filteredEmployees);
            }
        }
        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db,this);
        public DeleteCommand Delete => new DeleteCommand(_db,this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);

        public string Header => "Employees";
    }
}
