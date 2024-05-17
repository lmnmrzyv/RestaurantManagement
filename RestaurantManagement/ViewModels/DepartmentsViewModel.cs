using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands.Departments;
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
    public class DepartmentsViewModel:BaseViewModel
    {
        private readonly IUnitOfWork _db;
        public DepartmentsViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentDepartments=new DepartmentsModel();
            
        }
        private  State _state;
        public State CurrentState 
        { 
            get => _state;
            set
            {
                _state= value;
                OnPropertyChanged(nameof(CurrentState));
            }
        }
        private DepartmentsModel _currentDepartments;

        public DepartmentsModel CurrentDepartments
        {
            get => _currentDepartments;
            set
            {
                _currentDepartments= value;
                OnPropertyChanged(nameof(CurrentDepartments));
            }
        }

        private DepartmentsModel _selectedDepartments;

        public DepartmentsModel SelectedDepartments
        {
            get => _selectedDepartments;
            set
            {
                _selectedDepartments= value;
                if(_selectedDepartments!=null)
                {
                    CurrentDepartments.No=SelectedDepartments.No;
                    CurrentDepartments.Name=SelectedDepartments.Name;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentDepartments=new DepartmentsModel();
                    CurrentState=State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedDepartments));
            }
        }
        public ObservableCollection<DepartmentsModel> Departments { get; set; }

        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db,this);
        public DeleteCommand Delete => new DeleteCommand(this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
    }
}
