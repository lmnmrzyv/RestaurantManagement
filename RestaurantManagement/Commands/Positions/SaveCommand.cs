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

namespace RestaurantManagement.Commands.Positions
{
    public class SaveCommand : ICommand
    {
        private readonly PositionsViewModel _viewModel;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, PositionsViewModel viewmodel)
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
            var mapper = new PositionMapper();
            var positions = mapper.MapModelToEntity(new Position(), _viewModel.CurrentPosition);
            positions.IsActive = true;
            if (positions.Id == 0)
            {
                _viewModel.CurrentPosition.Id = _db.PositionRepository.Add(positions);
                var lastElementNo = _viewModel.Positions.LastOrDefault()?.No ?? 0;
                _viewModel.CurrentPosition.No = lastElementNo + 1;
                _viewModel.Positions.Add(_viewModel.CurrentPosition);
            }
            else
            {
                _db.PositionRepository.Update(positions);
                var updatedElement = _viewModel.Positions.First(x => x.Id == positions.Id);
                var index = _viewModel.Positions.IndexOf(updatedElement);
                _viewModel.Positions[index] = _viewModel.CurrentPosition;
            }
            _viewModel.SelectedPosition = null;
        }
    }
}
