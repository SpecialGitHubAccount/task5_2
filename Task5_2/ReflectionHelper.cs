using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Task5_2
{
    public static class ReflectionHelper
    {
        public static object GetPropertyValue(object instance, string propertyName)
        {
            //желательно предусмотреть чтобы propertyName мог быть составным (например address.city.region)
            string currentProp = string.Empty;
            int i = 0;
            while ((i = propertyName.IndexOf('.')) != -1)
            {
                i = propertyName.IndexOf('.');

                currentProp = propertyName.Substring(0, i);

                propertyName = propertyName.Substring(i + 1, (propertyName.Length - i - 1));

                instance = instance.GetType().GetProperty(currentProp).GetValue(instance);
            }

            return instance.GetType().GetProperty(propertyName).GetValue(instance, null);

        }

        public static object GetPropertyValueByType(object instance, string propertyType)
        {
            return instance.GetType().GetProperties().Where(pi => pi.PropertyType.Name == propertyType).First().GetValue(instance);
        }

        public static bool HasProperty(object instance, string propertyName)
        {
            //желательно предусмотреть чтобы propertyName мог быть составным (например address.city.region)
            string currentProp = string.Empty;
            int i = 0;
            while ((i = propertyName.IndexOf('.')) != -1)
            {
                i = propertyName.IndexOf('.');

                currentProp = propertyName.Substring(0, i);

                propertyName = propertyName.Substring(i + 1, (propertyName.Length - i - 1));

                instance = instance.GetType().GetProperty(currentProp).GetValue(instance);
            }

            return instance.GetType().GetProperty(propertyName) != null;
        }

        public static object GetPropertyValue(object instance, params string[] propertyNames)
        {
            //вернуть первый из найденых property с заданными именами.
            return instance.GetType().GetProperties().First(pi => propertyNames.Contains(pi.Name)).GetValue(instance);
        }

        public static PropertyInfo GetProperty(Type type, string propertyName)
        {
            return type.GetProperty(propertyName);
        }

        public static PropertyInfo GetProperty(object instance, string propertyName)
        {
            //желательно предусмотреть чтобы propertyName мог быть составным (например address.city.region)
            string currentProp = string.Empty;
            int i = 0;
            while ((i = propertyName.IndexOf('.')) != -1)
            {
                i = propertyName.IndexOf('.');

                currentProp = propertyName.Substring(0, i);

                propertyName = propertyName.Substring(i + 1, (propertyName.Length - i - 1));

                instance = instance.GetType().GetProperty(currentProp).GetValue(instance);
            }

            return instance.GetType().GetProperty(propertyName);
        }

        public static Type GetPropertyType(object instance, string propertyName)
        {
            //желательно предусмотреть чтобы propertyName мог быть составным (например address.city.region)

            string currentProp = string.Empty;
            int i = 0;
            while ((i = propertyName.IndexOf('.')) != -1)
            {
                i = propertyName.IndexOf('.');

                currentProp = propertyName.Substring(0, i);

                propertyName = propertyName.Substring(i + 1, (propertyName.Length - i - 1));

                instance = instance.GetType().GetProperty(currentProp).GetValue(instance);
            }

            return instance.GetType().GetProperty(propertyName).PropertyType;
        }

        public static List<Attribute> GetCustomAttributes(PropertyInfo property)
        {
            return property.GetCustomAttributes().ToList();
        }

        public static List<MethodInfo> GetMethodsInfo(object instance)
        {
            return instance.GetType().GetMethods().ToList();
        }

        public static List<FieldInfo> GetFieldsInfo(object instance)
        {
            return instance.GetType().GetFields().ToList();
        }

        public static object CallMethod(object instance, string methodName, object[] param)
        {
            return instance.GetType().GetMethod(methodName).Invoke(instance, param);
        }



    }
}
