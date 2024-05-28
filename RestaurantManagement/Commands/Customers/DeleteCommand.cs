using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Customers
{
    public class DeleteCommand : ICommand
    {
        
        private readonly IUnitOfWork _db;
        private readonly CustomersViewModel _viewModel;

        public DeleteCommand(IUnitOfWork db,CustomersViewModel viewModel)
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
