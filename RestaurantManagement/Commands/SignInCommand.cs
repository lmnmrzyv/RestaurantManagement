using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.ViewModels.Interfaces;
using RestaurantManagement.ViewModels;
using RestaurantManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using RestaurantManagement.Infrastructure.Interfaces;

namespace RestaurantManagement.Commands
{
    public class SignInCommand : ICommand
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IUnitOfWork _db;
        private readonly IHashCalculator _hashCalculator;
        public SignInCommand(LoginViewModel loginViewModel, IUnitOfWork db, IHashCalculator hashCalculator)
        {
            _loginViewModel = loginViewModel;
            _db = db;
            _hashCalculator = hashCalculator;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;

            if (passwordBox == null)
                return;

            var username = _loginViewModel.User.Username;
            var password = passwordBox.Password;

            var user = _db.UserRepository.GetByUsername(username);

            if (user == null)
            {
                _loginViewModel.ErrorVisibility = Visibility.Visible;
                return;
            }

            var passwordHash = _hashCalculator.Calculate(password);

            if (passwordHash != user.PasswordHash)
            {
                _loginViewModel.ErrorVisibility = Visibility.Visible;
                return;
            }

            var mainPageViewModel = new MainPageViewModel(_db);
            var mainPage = new MainPage();

            mainPage.DataContext = mainPageViewModel;

            mainPage.Show();
            _loginViewModel.Window.Close();
        }
    }
}
