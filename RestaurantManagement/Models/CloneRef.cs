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
                    propertyInfo.SetValue(clone, value);
                }
            }

            return clone;
        }
    }
}
