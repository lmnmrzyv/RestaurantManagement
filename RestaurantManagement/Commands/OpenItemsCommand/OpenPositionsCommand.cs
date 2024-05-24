using DataAccessManager.Domain.Entities;
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
    public class OpenPositionsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly IUnitOfWork _db;
        public OpenPositionsCommand(IUnitOfWork db)
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
            var control = new PositionsControl();
            var viewModel = new PositionsViewModel(_db);
            var positions = _db.PositionRepository.GetAll();
            var positionModels = new List<PositionsModel>();
            var positionMapper = new PositionMapper();
            var no = 1;
            foreach (var position in positions)
            {
                var positionModel = positionMapper.MapEntityToModel(position, new PositionsModel());
                positionModel.No = no++;
                positionModels.Add(positionModel);
            }
            viewModel.Positions = new ObservableCollection<PositionsModel>(positionModels);
            control.DataContext = viewModel;
            grid.Children.Add(control);
            var positionsControl = new PositionsControl();
            grid.Children.Add(positionsControl);
        }
    }
}
