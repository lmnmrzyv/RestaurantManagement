using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands;
using RestaurantManagement.Infrastructure.Interfaces;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels.Interfaces;
using RestaurantManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RestaurantManagement.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(IUnitOfWork db, IHashCalculator hashCalculator, LoginPage window)
        {
            SignIn = new SignInCommand(this, db, hashCalculator);
            Window = window;
        }

        public LoginPage Window { get; }

        private UserLoginModel _user;
        public UserLoginModel User
        {
            get
            {
                if (_user == null)
                    _user = new UserLoginModel();

                return _user;
            }
        }

        public SignInCommand SignIn { get; }

        private Visibility _errorVisibility = Visibility.Collapsed;
        public Visibility ErrorVisibility
        {
            get => _errorVisibility;
            set
            {
                _errorVisibility = value;
                OnPropertyChanged(nameof(ErrorVisibility));
            }
        }
    }
}
