using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class TablesModel : IModel,INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int _no;
        public int No
        {
            get => _no;
            set
            {
                _no = value;
                OnPropertyChanged(nameof(No));
            }
        }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TablesModel Clone()
        {
            var tableModel= new TablesModel();

            tableModel.Id = Id;
            tableModel.No = No;
            tableModel.TableNumber = TableNumber;
            tableModel.Capacity = Capacity;

            return tableModel;
        }
    }
}
