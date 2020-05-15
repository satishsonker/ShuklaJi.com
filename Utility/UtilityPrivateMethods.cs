using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace ShuklaJi.Utilities
{
    public static class UtilityPrivateMethods
    {
        #region Private Methods
        /// <summary>
        /// Deserialize JSON string to datatable
        /// </summary>
        /// <param name="json">String in JSON Format</param>
        /// <returns>Datatable</returns>
        internal static DataTable ConvertJsonToDataTable(string json)
        {
            var result = new DataTable();
            var jArray = JArray.Parse(json);
            //Initialize the columns, If you know the row type, replace this   
            foreach (var row in jArray)
            {
                foreach (var jToken in row)
                {
                    if (!(jToken is JProperty jproperty))
                    {
                        continue;
                    }

                    if (result.Columns[jproperty.Name.Trim()] == null)
                    {
                        result.Columns.Add(jproperty.Name.Trim(), typeof(string));
                    }
                }
            }
            foreach (var row in jArray)
            {
                var datarow = result.NewRow();
                foreach (var jToken in row)
                {
                    if (!(jToken is JProperty jProperty))
                    {
                        continue;
                    }

                    datarow[jProperty.Name.Trim()] = jProperty.Value.ToString();
                }
                result.Rows.Add(datarow);
            }

            return result;
        }

        internal static string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        #endregion
    }
}
