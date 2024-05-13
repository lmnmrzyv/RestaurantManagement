using System;
using System.Collections.Generic;
using DataAccessManager.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Models;
using System.Reflection;

namespace RestaurantManagement.Mappers
{
    public class Mapper<TModel, TEntity> where TEntity : IDbEntity
                                              where TModel : IModel
    {
        public  TEntity MapModelToEntity(TEntity entity, TModel model)
        {
            Type entitytype = entity.GetType();
            Type modelType = model.GetType();

            PropertyInfo[] propertyInfosEntity = entitytype.GetProperties();
            PropertyInfo[] propertyInfosModel = modelType.GetProperties();

            for (int i = 0; i < propertyInfosEntity.Length; i++)
            {
                for (int j = 0; j < propertyInfosModel.Length; j++)
                {
                    if (propertyInfosEntity[i].Name == propertyInfosModel[j].Name)
                    {

                        var value = propertyInfosModel[j].GetValue(model);
                        propertyInfosEntity[i].SetValue(entity, value);
                        Console.WriteLine($"{propertyInfosEntity[i].Name}: {value}");
                        break;
                    }



                }

            }
            return entity;
        }
        public  TModel MapEntityToModel(TEntity entity, TModel model)
        {
            Type entitytype = entity.GetType();
            Type modelType = model.GetType();

            PropertyInfo[] propertyInfosEntity = entitytype.GetProperties();
            PropertyInfo[] propertyInfosModel = modelType.GetProperties();

            for (int i = 0; i < propertyInfosEntity.Length; i++)
            {
                for (int j = 0; j < propertyInfosModel.Length; j++)
                {
                    if (propertyInfosEntity[i].Name == propertyInfosModel[j].Name)
                    {

                        var value = propertyInfosEntity[i].GetValue(entity);

                        propertyInfosModel[j].SetValue(model, value);
                        Console.WriteLine($"{propertyInfosModel[j].Name}: {value}");
                        break;
                    }


                }

            }
            return model;
        }
    }
}
