using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class PositionsModel : IModel
    {
        public int Id { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public PositionsModel Clone()
        {
            var positionModel = new PositionsModel();

            positionModel.Id = Id;
            positionModel.No = No;
            positionModel.Name = Name;
            positionModel.DepartmentId = DepartmentId;

            return positionModel;
        }
    }
}
