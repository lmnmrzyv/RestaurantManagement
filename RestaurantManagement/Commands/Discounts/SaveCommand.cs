using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using RestaurantManagement.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace RestaurantManagement.Commands.Discounts
{
    public class SaveCommand : ICommand
    {
        private readonly DiscountsViewModel _viewModel;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, DiscountsViewModel viewModel)
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
            var mapper = new DiscountMapper();
            var discount = mapper.MapModelToEntity(new Discount(), _viewModel.CurrentDiscount);
            discount.IsActive = true;
            if (discount.Id == 0)
            {
                _viewModel.CurrentDiscount.Id = _db.DiscountRepository.Add(discount);
                var lastElementNo = _viewModel.AllDiscounts.LastOrDefault()?.No ?? 0;
                _viewModel.CurrentDiscount.No = lastElementNo + 1;
                _viewModel.Discounts.Add(_viewModel.CurrentDiscount);
                _viewModel.AllDiscounts = _viewModel.Discounts.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var existingDiscount = _db.DiscountRepository.Get(discount.Id);
                _db.DiscountRepository.Update(discount);
                var index = _viewModel.Discounts.IndexOf(_viewModel.Discounts.First(x => x.Id == discount.Id));
                _viewModel.Discounts[index] = _viewModel.CurrentDiscount;
                _viewModel.AllDiscounts = _viewModel.Discounts.ToList();

                MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            _viewModel.CurrentDiscount = new DiscountModel();
            _viewModel.SelectedDiscount = null;
            _viewModel.CurrentState = State.NORMAL;
        }

    
    }
}
