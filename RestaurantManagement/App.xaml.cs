using DataAccessManager.DataAccess.SqlServer;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.ViewModels;
using RestaurantManagement.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RestaurantManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IUnitOfWork db = new SqlUnitOfWork("DESKTOP-0RTBK57", "RestaurantManagement");
            var mainPageViewModel = new MainPageViewModel(db);
            var mainPage = new MainPage
            {
                DataContext = mainPageViewModel
            };

            MainWindow = mainPage;

            MainWindow.Show();
        }
    }
}