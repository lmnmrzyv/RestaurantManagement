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
        private readonly TablesViewModel _viewModel;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, TablesViewModel viewmodel)
        {
            _db = db;
            _viewModel = viewmodel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var mapper = new TableMapper();
            var tables = mapper.MapModelToEntity(new Table(), _viewModel.CurrentTables);
            tables.IsActive= true;
            if(tables.Id==0)
            {
                _viewModel.CurrentTables.Id = _db.TableRepository.Add(tables);
                var lastElementNo = _viewModel.Tables.LastOrDefault()?.No ?? 0;
                _viewModel.CurrentTables.No = lastElementNo + 1;
                _viewModel.Tables.Add(_viewModel.CurrentTables);
            }
            else
            {
                // var existingTable= _db.TableRepository.Get(tables.Id);
                _db.TableRepository.Update(tables);
                var updatedElement = _viewModel.Tables.First(x => x.Id == tables.Id);
                var index=_viewModel.Tables.IndexOf(updatedElement);
                _viewModel.Tables[index]=_viewModel.CurrentTables;
            }
            _viewModel.SelectedTables = null;
        }
    }
}
