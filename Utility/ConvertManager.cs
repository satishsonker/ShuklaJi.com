using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace ShuklaJi.Utilities
{
    public static class ConvertManager
    {

        /// <summary>
        /// Deserialize JSON string to datatable
        /// </summary>
        /// <param name="jsonString">String in JSON Format</param>
        /// <returns>Datatable</returns>
        public static DataTable ToDataTable(string jsonString)
        {
            return UtilityPrivateMethods.ConvertJsonToDataTable(jsonString);
        }

        /// <summary>
        /// Return To Datable from List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        /// <summary>
        /// Serialize datatable in xml string
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <returns></returns>
        public static string ToXML(this DataTable dataTable)
        {
            MemoryStream str = new MemoryStream();
            dataTable.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// Common Methods to convert from datatable to list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt)
        {
            string dtJson = UtilityPrivateMethods.DataTableToJSONWithJSONNet(dt);
           return JsonConvert.DeserializeObject<List<T>>(dtJson);
        }

        /// <summary>
        /// Get the JSON string from Datatable
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string ToJson(this DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        /// <summary>
        /// Convert Datarow to list of T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="dr">DataRow</param>
        /// <returns></returns>
        public static T ToListItem<T>(this DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        switch (Type.GetTypeCode(pro.PropertyType))
                        {
                            case TypeCode.Int32:
                            case TypeCode.Int16:
                                pro.SetValue(obj, Convert.ToInt32(dr[column.ColumnName]), null);
                                break;
                            case TypeCode.Double:
                            case TypeCode.Decimal:
                                pro.SetValue(obj, Convert.ToDouble(dr[column.ColumnName]), null);
                                break;
                            case TypeCode.DateTime:
                                pro.SetValue(obj, Convert.ToDateTime(dr[column.ColumnName]), null);
                                break;
                            default:
                                pro.SetValue(obj, dr[column.ColumnName], null);
                                break;

                        }
                        //pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
