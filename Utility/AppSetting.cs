using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
/// <summary>
/// Get the Connection string from Appsettings.json
/// </summary>
namespace ShuklaJi.Utilities
{
    /// <summary>
    /// Read Connection string from Appsettings.json
    /// </summary>
    public static class AppSetting
    {
        #region Private Methods

        /// <summary>
        /// Get IConfigurationRoot object to read AppSetting.json
        /// </summary>
        private static IConfigurationRoot GetConfigurableRoot()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            return configurationBuilder.Build();
        }

        /// <summary>
        /// Get the value from Appsettings.json against the provided Parent and child key name
        /// </summary>
        /// <param name="Section">Key name that is present at root of the Appsettings.json</param>
        /// <param name="SubSection">Key name that is present under the Section key name</param>
        /// <param name="DefaultValue">Return default value if key is not available</param>
        /// <returns>Value against the key</returns>
        private static string GetAppSettingsKeyData(string Section, string SubSection, string DefaultValue)
        {
            var root = GetConfigurableRoot();
            if (root != null)
            {
                Section = string.IsNullOrEmpty(Section.ToLower().Trim()) ? "appsettings" : Section;
                if (Section.ToLower().Trim() != "appsettings")
                {
                    return root.GetSection(Section)?.GetSection(SubSection)?.Value ?? DefaultValue;
                }
                else
                {
                    return root.GetSection("appsettings").GetSection(Section)?.GetSection(SubSection)?.Value ?? DefaultValue;
                }
            }
            return DefaultValue;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Get the value from Appsettings.json against the provided key name
        /// </summary>
        /// <param name="KeyName">Key name that is present at root of the Appsettings.json</param>
        /// <param name="DefaultValue">Return default value if key is not available</param>
        /// <returns>Value against the key</returns>
        public static string GetAppSettingsData(string KeyName, string DefaultValue)
        {
            return GetAppSettingsKeyData(string.Empty, KeyName, DefaultValue);
        }

        /// <summary>
        ///  Get the value from Appsettings.json against the provided Parent and child key name
        /// </summary>
        /// <param name="ParentKeyName">Key name that is present at root of the Appsettings.json</param>
        /// <param name="ChildKeyName">Key name that is present under the Section key name</param>
        /// <param name="DefaultValue">Return default value if key is not available</param>
        /// <returns>Value against the key</returns>
        public static string GetAppSettingsData(string ParentKeyName, string ChildKeyName, string DefaultValue)
        {
            return GetAppSettingsKeyData(ParentKeyName, ChildKeyName, DefaultValue);
        }

        public static T GetAppSettingsData<T>(string ParentKeyName, string ChildKeyName, string DefaultValue = "") where T : IConvertible
        {
            try
            {
                string val = GetAppSettingsData(ParentKeyName, ChildKeyName, DefaultValue);

                return (T)Convert.ChangeType(val, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }

        }

        public static T GetAppSettingsData<T>(string Keyname) where T : class
        {
            try
            {
                string val = GetAppSettingsData("appsetting", Keyname);

               return JsonConvert.DeserializeObject<T>(val);
            }
            catch (Exception)
            {
                return default(T);
            }

        }

        /// <summary>
        /// Contains the core logic to read connectionString from AppSettings.json
        /// </summary>
        /// <param name="connectionName">Name of the connection string</param>
        /// <param name="connectionString">return actual connection string against provided connection string name</param>
        public static string GetConnectionString(string connectionName, ref int connectionTimeOut)
        {
            string connectionTime = string.Empty;
            string connectionString = string.Empty;
            connectionName = string.IsNullOrEmpty(connectionName) ?
                GetAppSettingsData(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.DefaultConnectionStringName, string.Empty) :
                            connectionName;

            connectionString = GetAppSettingsData(UtilityConstant.AppConfigConstant.ConnectionStrings, connectionName, string.Empty);

            if (Regex.IsMatch(connectionString, UtilityConstant.Regex.Base64) && !string.IsNullOrEmpty(connectionString))
            {
                connectionName = EncryptManager.Decrypt(connectionString);
            }
            connectionTime = GetAppSettingsData(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.CommandTimeout, string.Empty);
            int.TryParse(connectionTime, out int conTimeout);
            connectionTimeOut = conTimeout == 0 ? 300 : conTimeout;
            return connectionString;
        }

        /// <summary>
        /// Contains the core logic to read connectionString from AppSettings.json
        /// </summary>
        /// <param name="connectionName">Name of the connection string</param>
        public static string GetConnectionString(string connectionName)
        {
            int timeout = 0;
            return GetConnectionString(connectionName, ref timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime SetAppDate()
        {
            return DateTime.UtcNow;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get Connection timeout
        /// </summary>
        public static int ConnectionTimeOut => GetAppSettingsData<int>(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.ConnectionTimeout, string.Empty);

        /// <summary>
        /// Get Default connection String
        /// </summary>
        public static string ConnectionStringDefault => GetAppSettingsData(UtilityConstant.AppConfigConstant.ConnectionStrings, GetAppSettingsData(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.DefaultConnectionStringName, string.Empty), string.Empty);

        #endregion
    }
}
