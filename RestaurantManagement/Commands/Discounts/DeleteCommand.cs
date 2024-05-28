using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }
    }
}
