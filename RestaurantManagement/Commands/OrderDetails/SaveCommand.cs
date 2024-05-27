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

namespace RestaurantManagement.Commands.OrderDetails
{
    public class SaveCommand : ICommand
    {
        private readonly OrderDetailsViewModel _viewModel;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db, OrderDetailsViewModel viewmodel)
        {
            _db = db;
            _viewModel = viewmodel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var mapper = new OrderDetailMapper();

            var orderDetails = mapper.MapModelToEntity(new OrderDetail(), _viewModel.CurrentOrderDetails);

            orderDetails.IsActive = true;

            if (orderDetails.Id == 0)
            {
                _viewModel.CurrentOrderDetails.Id = _db.OrderDetailRepository.Add(orderDetails);
                var lastElementNo = _viewModel.OrderDetails.LastOrDefault()?.No ?? 0;
                _viewModel.CurrentOrderDetails.No = lastElementNo + 1;
                _viewModel.OrderDetails.Add(_viewModel.CurrentOrderDetails);
            }
            else
            {
                // var existingTable= _db.TableRepository.Get(tables.Id);
                _db.OrderDetailRepository.Update(orderDetails);
                var updatedElement = _viewModel.OrderDetails.First(x => x.Id == orderDetails.Id);
                var index = _viewModel.OrderDetails.IndexOf(updatedElement);
                _viewModel.OrderDetails[index] = _viewModel.CurrentOrderDetails;
            }
            _viewModel.SelectedOrderDetails = null;
        }   
    }
}
