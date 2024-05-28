using RestaurantManagement.Mappers;
using System;
using System.Reflection;

namespace RestaurantManagement.Models
{
    public class CloneRef<TModel> where TModel : class, new()
    {
        public TModel Clone(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            TModel clone = new TModel();
            Type modelType = model.GetType();
            PropertyInfo[] propertyInfosModel = modelType.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfosModel)
            {
                if (propertyInfo.CanWrite) 
                {
                    object value = propertyInfo.GetValue(model);
                    if (typeof(IModel).IsAssignableFrom(propertyInfo.PropertyType))
                    {
                        var cloneType = typeof(CloneRef<>).MakeGenericType(propertyInfo.PropertyType);
                        var mapper = Activator.CreateInstance(cloneType);
                        var cloneMethod = cloneType.GetMethod("Clone");
                        var nestedModel = value;
                        var cloneValue = cloneMethod.Invoke(mapper, new object[] { nestedModel });
                        propertyInfo.SetValue(clone, cloneValue);
                    }                       
                    propertyInfo.SetValue(clone, value);
                }
            }

            return clone;
        }
    }
}
