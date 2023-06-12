using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace HiStaffAPI.Extension
{

    public static class CollectionHelper
    {
        public static DataTable ToList<T>(this IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item);

                table.Rows.Add(row);
            }

            return table;
        }

        public static List<T> ToList<T>(this IList<DataRow> rows)
        {
            List<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public static List<T> ToList<T>(this DataTable table)
        {
            if (table == null)
                return null;

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
                rows.Add(row);

            return ToList<T>(rows);
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default;
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    if (prop != null)
                    {
                        try
                        {
                            dynamic value = row[column.ColumnName];
                            if (value.GetType() == typeof(DBNull))
                            {
                                value = null;
                            }
                            if (value != null)
                            {
                                switch (Type.GetTypeCode(prop.PropertyType))
                                {
                                    case TypeCode.Boolean:
                                        {
                                            if (value == null)
                                                value = false;
                                            value = value.ToString() == "-1";
                                            break;
                                        }

                                    case TypeCode.DateTime:
                                        {
                                            DateTime.TryParse(value.ToString(), out DateTime dateValue);
                                            value = dateValue;
                                            break;
                                        }

                                    case TypeCode.Double:
                                        {
                                            double.TryParse(value.ToString(), out double doubleValue);
                                            value = doubleValue;
                                            break;
                                        }

                                    case TypeCode.Decimal:
                                        {
                                            decimal.TryParse(value.ToString(), out Decimal decimalValue);
                                            value = decimalValue;
                                            break;
                                        }

                                    case TypeCode.Int16:
                                        {
                                            short.TryParse(value.ToString(), out short int16Value);
                                            value = int16Value;
                                            break;
                                        }
                                    case TypeCode.Int32:
                                        {
                                            int.TryParse(value.ToString(), out int int32Value);
                                            value = int32Value;
                                            break;
                                        }
                                    case TypeCode.String:
                                        {
                                            value = value.ToString();
                                            break;
                                        }

                                }
                            }

                            prop.SetValue(obj, value, null);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
            }

            return obj;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, prop.PropertyType);

            return table;
        }
    }

}