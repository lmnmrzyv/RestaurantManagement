using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using RestaurantManagement.Views.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RestaurantManagement.Commands
{
    public class OpenTablesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly IUnitOfWork _db;
        public OpenTablesCommand(IUnitOfWork db)
        {
            _db = db;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
       
        public void Execute(object parameter)
        {
            var grid = parameter as Grid;

            if (grid == null)
                return;

            grid.Children.Clear();

            var control = new TablesControl();
            var viewModel= new TablesViewModel(_db);
            var tables = _db.TableRepository.GetAll();
            var tableModels= new List<TableModel>();
            var tableMapper= new TableMapper();
            var no = 1;
            foreach ( var table in tables)
            {
                var tableModel = tableMapper.MapEntityToModel(table,new TableModel());
                tableModel.No = no++;
                tableModels.Add(tableModel);
            }
            viewModel.AllTables= tableModels;
            viewModel.Tables= new ObservableCollection<TableModel>(tableModels);
            control.DataContext = viewModel;
            grid.Children.Add(control);
        }
    }
}
