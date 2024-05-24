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
    public class OpenOrderDetailsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly IUnitOfWork _db;
        public OpenOrderDetailsCommand(IUnitOfWork db)
        {
            _db = db;
        }
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

            var control = new OrderDetailsControl();
            var viewModel = new OrderDetailsViewModel(_db);
            var orderDetails = _db.OrderDetailRepository.GetAll();
            var orderDetailsModels = new List<OrderDetailsModel>();
            var orderDetailMapper = new OrderDetailMapper();
            var no = 1;
            foreach (var orderDetail in orderDetails)
            {
                var orderDetailModel = orderDetailMapper.MapEntityToModel(orderDetail, new OrderDetailsModel());
                orderDetailModel.No = no++;
                orderDetailsModels.Add(orderDetailModel);
            }
            viewModel.OrderDetails = new ObservableCollection<OrderDetailsModel>(orderDetailsModels);
            control.DataContext = viewModel;
            grid.Children.Add(control);
        }
    }
}
