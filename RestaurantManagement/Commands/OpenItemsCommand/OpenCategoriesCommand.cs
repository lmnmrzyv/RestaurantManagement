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
    public class OpenCategoryCommand : ICommand
    {
        private readonly IUnitOfWork _db;
        public OpenCategoryCommand(IUnitOfWork db)
        {
            _db = db;
        }
        public event EventHandler CanExecuteChanged;

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

            var control = new CategoriesControl();
            var viewModel = new CategoriesViewModel(_db);

            var categories = _db.CategoryRepository.GetAll();

            var categoriesModel = new List<CategoriesModel>();

            var categoriesMapper = new CategoryMapper();
            var no = 1;

            foreach (var category in categories)
            {
                var categoryModel = categoriesMapper.MapEntityToModel(category, new CategoriesModel());

                categoryModel.No = no++;

                categoriesModel.Add(categoryModel);
            }

            viewModel.AllCategories = categoriesModel;
            viewModel.Categories = new ObservableCollection<CategoriesModel>(categoriesModel);

            control.DataContext = viewModel;

            grid.Children.Add(control);
        }
    }
}
