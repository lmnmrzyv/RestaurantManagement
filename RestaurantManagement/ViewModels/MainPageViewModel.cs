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
            OpenDepartments = new OpenDepartmentCommand(db);
            OpenEmployees = new OpenEmployeeCommand(db);
            OpenReservations = new OpenReservationCommand(db);
            OpenCategories = new OpenCategoryCommand(db);
            OpenMenuItems = new OpenMenuItemCommand(db);
            OpenOrders = new OpenOrdersCommand();
            OpenCustomers = new OpenCustomersCommand(db);
            OpenDiscounts = new OpenDiscountCommand(db);
            OpenPositions = new OpenPositionsCommand(db);
            OpenOrderDetails = new OpenOrderDetailsCommand(db);
            OpenTables = new OpenTablesCommand(db);
        }


        public OpenDepartmentCommand OpenDepartments { get; }
        public OpenEmployeeCommand OpenEmployees { get; }
        public OpenReservationCommand OpenReservations { get; }
        public OpenCategoryCommand OpenCategories { get; }
        public OpenMenuItemCommand OpenMenuItems { get; }
        public OpenOrdersCommand OpenOrders { get; }
        public OpenCustomersCommand OpenCustomers { get; }
        public OpenDiscountCommand OpenDiscounts { get; }
        public OpenOrderDetailsCommand OpenOrderDetails { get; }
        public OpenTablesCommand OpenTables { get; }
        public OpenPositionsCommand OpenPositions { get; }


    }
}
