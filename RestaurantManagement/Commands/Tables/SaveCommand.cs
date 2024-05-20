using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Tables
{
    public class SaveCommand : ICommand
    {
        private readonly TablesViewModel _currentState;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, TablesViewModel currentState)
        {
            _db = db;
            _currentState = currentState;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var mapper = new TableMapper();
            var tables = mapper.MapModelToEntity(new Table(),_currentState.CurrentTables);

            _db.TableRepository.Add(tables);

            var lastElementNo=_currentState.DTables.LastOrDefault()?.No ?? 0;

            _currentState.CurrentTables.No = lastElementNo+1;

            _currentState.DTables.Add(_currentState.CurrentTables);
            _currentState.CurrentTables = new TablesModel();

            _currentState.CurrentState = State.NORMAL;
        }
    }
}
