using DatabaseHandler.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace DatabaseHandler.Extensions
{
    public static class EnumerableExtensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> items, PropertyInfo[] props = null)
        {
            if(props == null) props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            DataTable dataTable = new DataTable();

            foreach (var prop in props)
            {          
                Type propType = prop.PropertyType;

                if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    propType = new NullableConverter(propType).UnderlyingType;

                dataTable.Columns.Add(prop.Name, propType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                    values[i] = props[i].GetValue(item, null);

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
