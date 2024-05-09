using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.ViewModels
{
    internal class MainPageViewModel
    {
        public MainPageViewModel(IUnitOfWork db)
        {
            OpenDepartments = new OpenDepartmentsCommand(db);
            OpenEmployees = new OpenEmployeesCommand(db);
            OpenReservations = new OpenReservationsCommand(db);
            OpenCategories = new OpenCategoriesCommand();
            OpenMenuItems = new OpenMenuItemsCommand();
            OpenOrders = new OpenOrdersCommand();
            OpenCustomers = new OpenCustomersCommand();
            OpenDiscounts = new OpenDiscountsCommand();
            OpenPositions = new OpenPositionsCommand();
            OpenOrderDetails = new OpenOrderDetailsCommand();
            OpenTables = new OpenTablesCommand();
        }


        public OpenDepartmentsCommand OpenDepartments { get; }
        public OpenEmployeesCommand OpenEmployees { get; }
        public OpenReservationsCommand OpenReservations { get; }
        public OpenCategoriesCommand OpenCategories { get; }
        public OpenMenuItemsCommand OpenMenuItems { get; }
        public OpenOrdersCommand OpenOrders { get; }
        public OpenCustomersCommand OpenCustomers { get; }
        public OpenDiscountsCommand OpenDiscounts { get; }
        public OpenOrderDetailsCommand OpenOrderDetails { get; }
        public OpenTablesCommand OpenTables { get; }
        public OpenPositionsCommand OpenPositions { get; }


    }
}
