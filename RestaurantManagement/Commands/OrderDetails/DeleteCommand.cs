using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.OrderDetails
{
    public class DeleteCommand : ICommand
    {
        private readonly OrderDetailsViewModel _viewModel;
        private readonly IUnitOfWork _db;
        public DeleteCommand(IUnitOfWork db, OrderDetailsViewModel currentState)
        {
            _db = db;
            _viewModel = currentState;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
           var result= MessageBox.Show("Are you sure you want to delete selected item?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
                return;

            var deletedId = _viewModel.SelectedOrderDetails.Id;
            _db.OrderDetailRepository.Delete(deletedId);

            MessageBox.Show("Successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _viewModel.OrderDetails.Remove(_viewModel.SelectedOrderDetails);
            for(int i=0; i<_viewModel.OrderDetails.Count; i++)
            {
                _viewModel.OrderDetails[i].No = i + 1;
            }
        }
    }
}
