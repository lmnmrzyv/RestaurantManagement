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
            CurrentDepartment = new DepartmentModel();
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
        private DepartmentModel _currentDepartment;
        public DepartmentModel CurrentDepartment
        {
            get => _currentDepartment;
            set
            {
                _currentDepartment = value;
                OnPropertyChanged(nameof(CurrentDepartment));
            }
        }
        private DepartmentModel _selectedDepartment;
        public DepartmentModel SelectedDepartment
        {
            get => _selectedDepartment;

            set
            {
                _selectedDepartment = value;
                if (_selectedDepartment != null)
                {
                    var DepartmentsTemp=new DepartmentModel();
                    DepartmentsTemp.Name = SelectedDepartment.Name;
                    DepartmentsTemp.Id = SelectedDepartment.Id;
                    DepartmentsTemp.No = SelectedDepartment.No;
                    CurrentDepartment=DepartmentsTemp;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentDepartment = new DepartmentModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedDepartment));
               
            }
        }
        private ObservableCollection<DepartmentModel> _departments { get; set; }
        public ObservableCollection<DepartmentModel> Departments
        {
            get => _departments;
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }


        public List<DepartmentModel> AllDepartments { get; set; }

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

                Departments = new ObservableCollection<DepartmentModel>(filteredDepartments);
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
