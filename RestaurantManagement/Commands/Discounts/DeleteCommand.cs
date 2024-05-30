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

namespace RestaurantManagement.Commands.Discounts
{
    public class DeleteCommand : ICommand
    {
        private readonly IUnitOfWork _db;
        private readonly DiscountsViewModel _viewModel;
        public DeleteCommand(IUnitOfWork db , DiscountsViewModel viewModel)
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

            if (result == MessageBoxResult.No)
                return;

            var deletedId = _viewModel.SelectedDiscount.Id;

            _db.DiscountRepository.Delete(deletedId);

            MessageBox.Show("Successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _viewModel.Discounts.Remove(_viewModel.SelectedDiscount);
            _viewModel.AllDiscounts = _viewModel.Discounts.ToList();

            for (int i = 0; i < _viewModel.Discounts.Count; i++)
            {
                _viewModel.Discounts[i].No = i + 1;
            }
            _viewModel.Discounts = new ObservableCollection<DiscountModel>(_viewModel.Discounts);
            _viewModel.CurrentState = State.NORMAL;
        }
    }
}
