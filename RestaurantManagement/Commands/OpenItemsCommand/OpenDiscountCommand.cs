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
    public class OpenDiscountCommand : ICommand
    {
        private readonly IUnitOfWork _db;
        public OpenDiscountCommand(IUnitOfWork db)
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

            var control = new DiscountsControl();
            var viewModel = new DiscountsViewModel(_db);

            var discounts = _db.DiscountRepository.GetAll();

            var discountsModel = new List<DiscountModel>();

            var discountsMapper = new DiscountMapper();
            var no = 1;

            foreach (var discount in discounts)
            {
                var discountModel = discountsMapper.MapEntityToModel(discount,new DiscountModel());

                discountModel.No = no++;

                discountsModel.Add(discountModel);
            }

            viewModel.AllDiscounts = discountsModel;
            viewModel.Discounts = new ObservableCollection<DiscountModel>(discountsModel);

            control.DataContext = viewModel;

            grid.Children.Add(control);

        }
    }
}
