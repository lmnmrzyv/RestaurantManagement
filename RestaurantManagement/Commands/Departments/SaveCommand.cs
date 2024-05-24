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

namespace RestaurantManagement.Commands.Departments
{
    public class SaveCommand : ICommand
    {
        private readonly DepartmentsViewModel _currentState;
        private readonly IUnitOfWork _db;
        public SaveCommand(IUnitOfWork db,DepartmentsViewModel currentState)
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
            var mapper = new DepartmentMapper();
            var department = mapper.MapModelToEntity(new Department(), _currentState.CurrentDepartments);

            // Yeni bir departman mı oluşturuluyor yoksa mevcut bir departman mı güncelleniyor kontrol et
            if (department.Id == 0)
            {
                // Yeni departmanı veritabanına ekle ve dönen ID ile modeli güncelle
                _currentState.CurrentDepartments.Id = _db.DepartmentRepository.Add(department);

                // Departmanın yeni numarasını belirle
                var lastElementNo = _currentState.Departments.LastOrDefault()?.No ?? 0;
                _currentState.CurrentDepartments.No = lastElementNo + 1;

                // Mevcut departman modelini observable koleksiyona ekle
                _currentState.Departments.Add(_currentState.CurrentDepartments);

                MessageBox.Show("Başarıyla oluşturuldu", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Gerekirse mevcut departmanı veritabanından al
                var existingDepartment = _db.DepartmentRepository.Get(department.Id);

                // Mevcut departmanı veritabanında güncelle
                _db.DepartmentRepository.Update(department);

                // Observable koleksiyondaki departmanın indeksini bul
                var index = _currentState.Departments.IndexOf(_currentState.Departments.First(x => x.Id == department.Id));

                // Observable koleksiyondaki departman modelini güncelle
                _currentState.Departments[index] = _currentState.CurrentDepartments;

                MessageBox.Show("Başarıyla güncellendi", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Mevcut departman modelini sıfırla
            _currentState.CurrentDepartments = new DepartmentsModel();
            _currentState.SelectedDepartments = null;
            _currentState.CurrentState = State.NORMAL;
        }

    }
}
