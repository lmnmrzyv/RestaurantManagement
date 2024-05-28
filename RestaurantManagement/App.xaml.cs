using DataAccessManager.DataAccess.SqlServer;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Implementations;
using RestaurantManagement.Infrastructure.Interfaces;
using RestaurantManagement.ViewModels;
using RestaurantManagement.ViewModels.Interfaces;
using RestaurantManagement.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace RestaurantManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            /*  var dbAddress = ConfigurationManager.AppSettings["databaseAddress"];
              var dbName = ConfigurationManager.AppSettings["databaseName"];*/
            IConfigManager configManager = new FileConfigManager();

            var databaseConfigModel = configManager.GetDatabaseConfig();

            IUnitOfWork db = new SqlUnitOfWork(databaseConfigModel.DBAddress, databaseConfigModel.DBName);
            IHashCalculator hashCalculator = new MD5HashCalculator();
            var loginPage = new LoginPage();
            var loginViewModel = new LoginViewModel(db, hashCalculator, loginPage);

            loginPage.DataContext = loginViewModel;

            MainWindow = loginPage;
            Dispatcher.UnhandledException += OnUnhandledException;
            MainWindow.Show();
           /* var mainPageViewModel = new MainPageViewModel(db);
            var mainPage = new MainPage
            {
                DataContext = mainPageViewModel
            };

            MainWindow = mainPage;

            MainWindow.Show()*/;
        }
        private void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            MessageBox.Show("Something went wrong, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}