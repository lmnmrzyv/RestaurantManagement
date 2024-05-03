using System;
using System.Collections.Generic;
using DataAccessManager.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Models;

namespace RestaurantManagement.Mappers
{
    public interface IMapper<TModel, TEntity> where TEntity : IDbEntity
                                              where TModel : IModel
    {
        TModel Map(TEntity entity);
        TEntity Map(TModel model);
    }
}
