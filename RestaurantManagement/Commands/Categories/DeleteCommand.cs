using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Categories
{
    public class DeleteCommand : ICommand
    {
        private readonly IUnitOfWork _db;
        private readonly CategoriesViewModel _viewModel;
        public DeleteCommand(IUnitOfWork db, CategoriesViewModel viewModel)
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
            var result = MessageBox.Show("Are you sure you want to delete selected item?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            var deletedId = _viewModel.SelectedCategory.Id;

            _db.CategoryRepository.Delete(deletedId);

            MessageBox.Show("Successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _viewModel.Categories.Remove(_viewModel.SelectedCategory);
            _viewModel.AllCategories = _viewModel.Categories.ToList();

            for (int i = 0; i < _viewModel.Categories.Count; i++)
            {
                _viewModel.Categories[i].No = i + 1;
            }
            _viewModel.Categories = new ObservableCollection<CategoriesModel>(_viewModel.Categories);
            _viewModel.CurrentState = State.NORMAL;
        }
    }
}
