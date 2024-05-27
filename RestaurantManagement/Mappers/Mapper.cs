using System;
using System.Collections.Generic;
using DataAccessManager.Domain.Entities;
using RestaurantManagement.Models;
using System.Reflection;

namespace RestaurantManagement.Mappers
{
    public class Mapper<TModel, TEntity>
        where TEntity : IDbEntity
        where TModel : IModel
    {
        public TEntity MapModelToEntity(TEntity entity, TModel model)
        {
            Type entityType = entity.GetType();
            Type modelType = model.GetType();

            PropertyInfo[] propertyInfosEntity = entityType.GetProperties();
            PropertyInfo[] propertyInfosModel = modelType.GetProperties();

            for (int i = 0; i < propertyInfosEntity.Length; i++)
            {
                for (int j = 0; j < propertyInfosModel.Length; j++)
                {
                    if (propertyInfosEntity[i].Name == propertyInfosModel[j].Name)
                    {
                        var modelValue = propertyInfosModel[j].GetValue(model);
                        if (modelValue != null)
                        {
                            if (propertyInfosEntity[i].PropertyType.IsEnum)
                            {
                                var enumValue = Enum.Parse(propertyInfosEntity[i].PropertyType, modelValue.ToString());
                                propertyInfosEntity[i].SetValue(entity, enumValue);
                            }
                            else if (typeof(IModel).IsAssignableFrom(propertyInfosModel[j].PropertyType) && typeof(IDbEntity).IsAssignableFrom(propertyInfosEntity[i].PropertyType))
                            {
                                var mapperType = typeof(Mapper<,>).MakeGenericType(propertyInfosModel[j].PropertyType, propertyInfosEntity[i].PropertyType);
                                var mapper = Activator.CreateInstance(mapperType);
                                var mapMethod = mapperType.GetMethod("MapModelToEntity");
                                var nestedEntity = Activator.CreateInstance(propertyInfosEntity[i].PropertyType);
                                var nestedModel = modelValue;
                                var mapperValue = mapMethod.Invoke(mapper, new object[] { nestedEntity, nestedModel });
                                propertyInfosEntity[i].SetValue(entity, mapperValue);
                            }
                            else
                            {
                                propertyInfosEntity[i].SetValue(entity, modelValue);
                            }
                        }
                        break;
                    }
                }
            }
            return entity;
        }

        public TModel MapEntityToModel(TEntity entity, TModel model)
        {
            Type entityType = entity.GetType();
            Type modelType = model.GetType();

            PropertyInfo[] propertyInfosEntity = entityType.GetProperties();
            PropertyInfo[] propertyInfosModel = modelType.GetProperties();

            for (int i = 0; i < propertyInfosEntity.Length; i++)
            {
                for (int j = 0; j < propertyInfosModel.Length; j++)
                {
                    if (propertyInfosEntity[i].Name == propertyInfosModel[j].Name)
                    {
                        var entityValue = propertyInfosEntity[i].GetValue(entity);
                        
                        if (entityValue != null)
                        {
                            if (propertyInfosModel[j].PropertyType.IsEnum)
                            {
                                var enumValue = Enum.Parse(propertyInfosModel[j].PropertyType, entityValue.ToString());
                                propertyInfosModel[j].SetValue(model, enumValue);
                            }
                            else if (typeof(IModel).IsAssignableFrom(propertyInfosModel[j].PropertyType) && typeof(IDbEntity).IsAssignableFrom(propertyInfosEntity[i].PropertyType))
                            {
                                var mapperType=typeof(Mapper<,>).MakeGenericType(propertyInfosModel[j].PropertyType,propertyInfosEntity[i].PropertyType);
                                var mapper=Activator.CreateInstance(mapperType);
                                var mapMethod= mapperType.GetMethod("MapEntityToModel");
                                var nestedModel=Activator.CreateInstance(propertyInfosModel[j].PropertyType);
                                var nestedEntity = entityValue;
                                var mapperValue=mapMethod.Invoke(mapper, new object[] { nestedEntity, nestedModel });
                                propertyInfosModel[j].SetValue(model, mapperValue);
                            }
                            else
                            {
                                propertyInfosModel[j].SetValue(model, entityValue);
                            }
                        }
                        break;
                    }
                }
            }
            return model;
        }
    }
}
