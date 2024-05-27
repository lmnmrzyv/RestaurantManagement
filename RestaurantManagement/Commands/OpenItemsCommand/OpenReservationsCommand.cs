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
    public class OpenReservationsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly IUnitOfWork _db;
        public OpenReservationsCommand(IUnitOfWork db)
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

            var reservationsControl = new ReservationsControl();
            var viewModel = new ReservationsViewModel(_db);
            viewModel.LoadTables();
            viewModel.LoadCustomers();
            var reservations=_db.ReservationRepository.GetAll();
            var reservationsModel=new List<ReservationModel>();
            var reservationMapper = new ReservationMapper();
            var no = 1;

            foreach (var reservation in reservations)
            {
                var reservationModel = reservationMapper.MapEntityToModel(reservation,new ReservationModel());
                reservationModel.No = no++;
                reservationsModel.Add(reservationModel);
            }
            viewModel.AllReservations= reservationsModel;
            viewModel.Reservations= new ObservableCollection<ReservationModel>(reservationsModel);
            reservationsControl.DataContext= viewModel;
            grid.Children.Add(reservationsControl);
        }
    }
}
