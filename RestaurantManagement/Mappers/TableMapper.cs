using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessManager.Domain.Entities;
using RestaurantManagement.Models;

namespace RestaurantManagement.Mappers
{
    public class TableMapper : IMapper<TablesModel, Table>
    {
        public TablesModel Map(Table entity)
        {
            var tableModel = new TablesModel();

            tableModel.TableNumber = entity.TableNumber;
            tableModel.Capacity = entity.Capacity;


            return tableModel;
        }

        public Table Map(TablesModel model)
        {
            var table = new Table();

            table.TableNumber = model.TableNumber;
            table.Capacity = model.Capacity;


            return table;
        }
    }
}
