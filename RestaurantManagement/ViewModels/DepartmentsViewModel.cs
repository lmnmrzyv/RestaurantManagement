using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands.Departments;
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
    public class DepartmentsViewModel : BaseViewModel, IControl
    {

        private readonly IUnitOfWork _db;
        public DepartmentsViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentDepartments = new DepartmentsModel();
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
        private DepartmentsModel _currentDepartments;
        public DepartmentsModel CurrentDepartments
        {
            get => _currentDepartments;
            set
            {
                _currentDepartments = value;
                OnPropertyChanged(nameof(CurrentDepartments));
            }
        }
        private DepartmentsModel _selectedDepartments;
        public DepartmentsModel SelectedDepartments
        {
            get => _selectedDepartments;

            set
            {
                _selectedDepartments = value;
                if (_selectedDepartments != null)
                {
                    CurrentDepartments.Name = SelectedDepartments.Name;
                    CurrentDepartments.Id = SelectedDepartments.Id;
                    CurrentDepartments.No = SelectedDepartments.No;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentDepartments = new DepartmentsModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedDepartments));
                OnPropertyChanged(nameof(CurrentDepartments));
            }
        }
        private ObservableCollection<DepartmentsModel> _departments { get; set; }
        public ObservableCollection<DepartmentsModel> Departments
        {
            get => _departments;
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }


        public List<DepartmentsModel> AllDepartments { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                var lowerSearchText = SearchText.ToLower();

                var filteredDepartments = AllDepartments.Where(x => x.Name.ToLower().Contains(lowerSearchText));

                Departments = new ObservableCollection<DepartmentsModel>(filteredDepartments);
            }
        }
        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(_db, this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);

        public string Header => "Departments";
    }
}
