using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands.Tables;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels.Interfaces;

namespace RestaurantManagement.ViewModels
{
    public class TablesViewModel: BaseViewModel, IControl
    {
        private readonly IUnitOfWork _db;
        public TablesViewModel(IUnitOfWork db)
        {
           _db = db;
            CurrentTables = new TableModel();
            LoadPositions();
        }
        /*public List<TablesModel> Tables { get; set; }*/
        
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
        private TableModel _currentTables;

        public TableModel CurrentTables
        {
            get => _currentTables;
            set
            {
                _currentTables = value;
                OnPropertyChanged(nameof(CurrentTables));
            }
        }

        private TableModel _selectedTables;

        public TableModel SelectedTables
        {
            get => _selectedTables;
            set
            {
                _selectedTables = value;
                if (_selectedTables != null)
                {
                    CurrentTables=SelectedTables.Clone();
                   // CurrentTables=SelectedTables.Clone();
                    /*CurrentTables.No = SelectedTables.No;
                    CurrentTables.TableNumber = SelectedTables.TableNumber;
                    CurrentTables.Capacity = SelectedTables.Capacity;*/
                    CurrentState = State.SELECTED;
                }
                else
                {
                    CurrentTables = new TableModel();
                    CurrentState = State.NORMAL;
                }
                OnPropertyChanged(nameof(SelectedTables));
            }
        }
        private ObservableCollection<TableModel> _tables { get; set; }

        public ObservableCollection<TableModel> Tables
        {
            get => _tables;
            set
            {
                _tables = value;
                OnPropertyChanged(nameof(Tables));
            }
        }
        public List<TableModel> AllTables { get; set; } = new List<TableModel>();
        private void LoadPositions()
        {
            var tableEntities = _db.TableRepository.GetAll();
            AllTables = tableEntities.Select(e => new TableModel
            {
                Id = e.Id,
                TableNumber = e.TableNumber,
                Capacity = e.Capacity
            }
            ).ToList();

            Tables = new ObservableCollection<TableModel>(AllTables);
        }
        private int _searchText;
        public int SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                var lowerSearchText = SearchText;

                var filteredTables = AllTables.Where(x => x.TableNumber == lowerSearchText ||
                                                       x.Capacity == lowerSearchText );
                Tables = new ObservableCollection<TableModel>(filteredTables);
            }
        }
      

        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(_db, this);
        public DeleteCommand Delete => new DeleteCommand(_db,this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);

        public string Header => "Tables";

        //public object Tables { get; internal set; }
    }
}
