using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;

namespace RestaurantManagement.ViewModels
{
    public class TablesViewModel:BaseViewModel
    {
        private readonly IUnitOfWork _db;
        public TablesViewModel(IUnitOfWork db)
        {
            _db = db;
            CurrentTables = new TablesModel();
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
        private TablesModel _currentTables;

        public TablesModel CurrentTables
        {
            get => _currentTables;
            set
            {
                _currentTables = value;
                OnPropertyChanged(nameof(CurrentTables));
            }
        }

        private TablesModel _selectedTables;

        public TablesModel SelectedTables
        {
            get => _selectedTables;
            set
            {
                _selectedTables = value;
                if (_selectedTables != null)
                {
                    CurrentTables.No = SelectedTables.No;
                    CurrentTables.TableNumber = SelectedTables.TableNumber;
                    CurrentTables.Capacity = SelectedTables.Capacity;
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentTables = new TablesModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedTables));
            }
        }
        public ObservableCollection<TablesModel> DTables { get; set; }

        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);
    }
}
