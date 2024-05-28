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
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Categories
{
    public class SaveCommand : ICommand
    {
        private readonly CategoriesViewModel _viewModel;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, CategoriesViewModel viewModel)
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
            var mapper = new CategoryMapper();
            var category = mapper.MapModelToEntity(new Category(), _viewModel.CurrentCategory);
            if (category.Id == 0)
            {
                _viewModel.CurrentCategory.Id = _db.CategoryRepository.Add(category);
                var lastElementNo = _viewModel.Categories.LastOrDefault()?.No ?? 0;
                _viewModel.CurrentCategory.No = lastElementNo + 1;
                _viewModel.Categories.Add(_viewModel.CurrentCategory);
                _viewModel.AllCategories = _viewModel.Categories.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var existingCategory = _db.CategoryRepository.Get(category.Id);
                _db.CategoryRepository.Update(category);
                var index = _viewModel.Categories.IndexOf(_viewModel.Categories.First(x => x.Id == category.Id));
                _viewModel.Categories[index] = _viewModel.CurrentCategory;
                _viewModel.AllCategories = _viewModel.Categories.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            _viewModel.CurrentCategory = new CategoriesModel();
            _viewModel.SelectedCategory = null;
            _viewModel.CurrentState = State.NORMAL;
        }

    }
}
