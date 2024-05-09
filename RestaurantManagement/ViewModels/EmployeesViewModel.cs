using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Commands.Employees;
using RestaurantManagement.Enums;
using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.ViewModels
{
    public class EmployeesViewModel:BaseViewModel
    {
        private readonly IUnitOfWork _db;

        public EmployeesViewModel(IUnitOfWork db)
        {
            _db = db;
        }

        private State _state;

        public State CurrentState
        {
            get=> _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(CurrentState));
            }
        }

        public List<EmployeesModel> Employees { get; set; }

        public AddCommand Add => new AddCommand(this);
        public SaveCommand Save => new SaveCommand(this);
        public DeleteCommand Delete => new DeleteCommand(this);
        public RejectCommand Reject => new RejectCommand(this);
        public EditCommand Edit => new EditCommand(this);

    }
}
