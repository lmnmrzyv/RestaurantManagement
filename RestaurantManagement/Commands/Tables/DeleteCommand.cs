using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Tables
{
    public class DeleteCommand : ICommand
    {
        private readonly TablesViewModel _viewModel;
        private readonly IUnitOfWork _db;
        public DeleteCommand(IUnitOfWork db,TablesViewModel viewModel)
        {
            _db = db;
            _viewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
           var result= MessageBox.Show("Are you sure you want to delete selected item?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
                return;
            var deletedId = _viewModel.SelectedTables.Id;
            _db.TableRepository.Delete(deletedId);
            _viewModel.Tables.Remove(_viewModel.SelectedTables);
            for(int i=0; i<_viewModel.Tables.Count; i++)
            {
                _viewModel.Tables[i].No = i + 1;
            }
        }
    }
}
