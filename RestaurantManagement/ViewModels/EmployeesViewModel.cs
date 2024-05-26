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
        public ObservableCollection<Position> Positions {  get; set; }
        public EmployeesViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentEmployee=new EmployeeModel();
            CurrentEmployee.Position = new Position();

            LoadPositions();
        }

        private void LoadPositions()
        {
            var positions = _db.PositionRepository.GetAll();

            Positions = new ObservableCollection<Position>(positions);
        }
        private State _currentState;

        public State CurrentState
        {
            get=> _currentState;
            set
            {
                _currentState = value;
                OnPropertyChanged(nameof(CurrentState));
            }
        }
       
        private EmployeeModel _selectedEmployee;
        public EmployeeModel SelectedEmployee
        {
            get => _selectedEmployee;

            set
            {
               _selectedEmployee= value;
                if(_selectedEmployee!=null)
                {
                   var Employeetmp = new EmployeeModel();
                    Employeetmp.EducationLevel = SelectedEmployee.EducationLevel;
                    Employeetmp.Position = SelectedEmployee.Position;
                    Employeetmp.PerformanceRating = SelectedEmployee.PerformanceRating;
                    Employeetmp.Surname= SelectedEmployee.Surname;
                    Employeetmp.Name= SelectedEmployee.Name;
                    Employeetmp.Id=SelectedEmployee.Id;
                    Employeetmp.No=SelectedEmployee.No;
                    CurrentEmployee = Employeetmp;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentEmployee = new EmployeeModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedEmployee));
               // OnPropertyChanged(nameof(CurrentEmployee));
                
            }
        }

        private EmployeeModel _currentEmployee;
        public EmployeeModel CurrentEmployee
        {
            get => _currentEmployee;
            set
            {
                _currentEmployee = value;
                OnPropertyChanged(nameof(CurrentEmployee));
            }
        }
        private ObservableCollection<EmployeeModel> _employees { get; set; }
        public ObservableCollection<EmployeeModel> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        public List<EducationLevel> EducationLevels => Enum.GetValues(typeof(EducationLevel)).Cast<EducationLevel>().ToList();

        public List<EmployeeModel> AllEmployees { get; set; }

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
                                                       (x.Surname.ToLower().Contains(lowerSearchText)) || (x.Position.Name.ToLower().Contains(lowerSearchText)) || (x.EducationLevel.ToString().ToLower().Contains(lowerSearchText)) || (x.PerformanceRating.ToString().ToLower().Contains(lowerSearchText)));

                Employees = new ObservableCollection<EmployeeModel>(filteredEmployees);
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
