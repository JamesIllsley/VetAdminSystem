using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace VetAdminSystem.Common
{
    public static class UsefulMethods
    {
        public static List<string> GetModelProperties<T>()
            where T : class
        {
            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(T).GetProperties();
            Array.Sort(propertyInfos,
                delegate (PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
                {
                    return propertyInfo1.Name.CompareTo(propertyInfo2.Name);
                });
            List<string> ModelProperties = new List<string>();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                ModelProperties.Add(propertyInfo.Name);
            }

            return ModelProperties;
        }
    }
}