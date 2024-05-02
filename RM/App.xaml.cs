using RM.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
         

            var mainPageViewModel = new MainPageViewModel();
            var mainPage = new MainWindow();

            mainPage.DataContext = mainPageViewModel;

            MainWindow = mainPage;

            MainWindow.Show();
        }
    }
}
