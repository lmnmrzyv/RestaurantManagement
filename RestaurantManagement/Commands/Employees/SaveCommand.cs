using DataAccessManager.Domain.Entities;
using DataAccessManager.Domain.Interfaces;
using RestaurantManagement.Enums;
using RestaurantManagement.Mappers;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RestaurantManagement.Commands.Employees
{
    public class SaveCommand : ICommand
    {
        private readonly EmployeesViewModel _currentState;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db,EmployeesViewModel currentState)
        {
            _db = db;
            _currentState = currentState;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var mapper = new EmployeeMapper();
            var employee = mapper.MapModelToEntity(new Employee(), _currentState.CurrentEmployee);
            employee.IsActive=true;
            // Yeni bir departman mı oluşturuluyor yoksa mevcut bir departman mı güncelleniyor kontrol et
            if (employee.Id == 0)
            {
                // Yeni departmanı veritabanına ekle ve dönen ID ile modeli güncelle
                _currentState.CurrentEmployee.Id = _db.EmployeeRepository.Add(employee);

                // Departmanın yeni numarasını belirle
                var lastElementNo = _currentState.AllEmployees.LastOrDefault()?.No ?? 0;
                _currentState.CurrentEmployee.No = lastElementNo + 1;

                // Mevcut departman modelini observable koleksiyona ekle
                _currentState.Employees.Add(_currentState.CurrentEmployee);

                MessageBox.Show("Başarıyla oluşturuldu", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Gerekirse mevcut departmanı veritabanından al
                var existingDepartment = _db.EmployeeRepository.Get(employee.Id);

                // Mevcut departmanı veritabanında güncelle
                _db.EmployeeRepository.Update(employee);

                // Observable koleksiyondaki departmanın indeksini bul
                var index = _currentState.Employees.IndexOf(_currentState.Employees.First(x => x.Id == employee.Id));

                // Observable koleksiyondaki departman modelini güncelle
                _currentState.Employees[index] = _currentState.CurrentEmployee;

                MessageBox.Show("Başarıyla güncellendi", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Mevcut departman modelini sıfırla
           // _currentState.CurrentDepartments = new EmployeesModel();
           // _currentState.SelectedDepartments = null;
            _currentState.CurrentState = State.NORMAL;
        }
    }
}
