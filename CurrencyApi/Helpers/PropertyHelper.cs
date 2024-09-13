using System;
using System.Reflection;

namespace CurrencyApi.Helpers
{
    /// <summary>
    /// The property helper.
    /// </summary>
    public class PropertyHelper
    {
        private static dynamic Find(dynamic pattern, IEnumerable<dynamic> source)
        {
            foreach (var obj in source)
            {
                foreach (var property in obj.GetType().GetProperties())
                {
                    PropertyInfo p = (PropertyInfo)property;
                    if (pattern.ToLower() == p.Name.ToString().ToLower())
                    {
                        return property.GetValue(obj, null);
                    }

                }
            }
            return null;
        }

        public static double FindPropertValueinClass(string propertyName, dynamic obj)
        {
            var toLookup = new List<dynamic> { obj };

            return (double)Find(propertyName, toLookup);
        }
    }
}
