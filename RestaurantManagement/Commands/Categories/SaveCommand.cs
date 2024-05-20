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

namespace RestaurantManagement.Commands.Categories
{
    public class SaveCommand : ICommand
    {
        private readonly CategoriesViewModel _currentState;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, CategoriesViewModel currentState)
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
            var mapper = new CategoryMapper();
            var categories = mapper.MapModelToEntity(new Category(), _currentState.CurrentCategories);

            _db.CategoryRepository.Add(categories);

            var lastElementNo = _currentState.Categories.LastOrDefault()?.No ?? 0;

            _currentState.CurrentCategories.No = lastElementNo + 1;

            _currentState.Categories.Add(_currentState.CurrentCategories);
            _currentState.CurrentCategories = new CategoriesModel();

            _currentState.CurrentState = State.NORMAL;
        }
    }
}
