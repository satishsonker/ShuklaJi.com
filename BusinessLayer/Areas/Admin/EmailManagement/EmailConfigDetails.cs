using ShuklaJi.DataManager;
using System.Data;
using ShuklaJi.Utilities;
using System.Collections.Generic;
using ShuklaJi.ModelLayer.Areas.Admin.EmailManagement;

namespace ShuklaJi.BusinessLayer.Areas.Admin.EmailManagement
{
    public class EmailConfigDetails
    {
        #region COTR
        public EmailConfigDetails()
        {
            DataExecutor.ConnectionString = Utilities.AppSetting.GetConnectionString(Utilities.AppSetting.GetAppSettingsData(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.DefaultConnectionStringName, string.Empty));
        }
        #endregion
        public DataTable GetEmailConfig(string ConfigName)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@ConfigName",ConfigName)
            };
            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetEmailConfig, param);
        }

        public string SaveEmailConfig(EmailConfigModel emailConfigModel)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@ConfigName",emailConfigModel.ConfigName),
                new KeyValuePair<string, object>("@ServerName",emailConfigModel.ServerName),
                new KeyValuePair<string, object>("@Port",emailConfigModel.Port),
                new KeyValuePair<string, object>("@Username",emailConfigModel.UserName),
                new KeyValuePair<string, object>("@Password",emailConfigModel.Password),
                new KeyValuePair<string, object>("@UserId",emailConfigModel.UserId),

            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_SaveEmailConfig, param);
        }

        public string UpdateEmailConfig(EmailConfigModel emailConfigModel)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@ConfigName",emailConfigModel.ConfigName),
                new KeyValuePair<string, object>("@ServerName",emailConfigModel.ServerName),
                new KeyValuePair<string, object>("@Port",emailConfigModel.Port),
                new KeyValuePair<string, object>("@Username",emailConfigModel.UserName),
                new KeyValuePair<string, object>("@Password",emailConfigModel.Password),
                new KeyValuePair<string, object>("@UserId",emailConfigModel.UserId),
                 new KeyValuePair<string, object>("@ConfigId",emailConfigModel.ConfigId),

            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_UpdateEmailConfig, param);
        }

        public string DeleteEmailConfig(int configId,int userId)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@ConfigId",configId),
                new KeyValuePair<string, object>("@UserId",userId)
            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_DeleteEmailConfig, param);
        }

        public string SetDefaultEmailConfig(int configId, int userId,bool status)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@ConfigId",configId),
                new KeyValuePair<string, object>("@UserId",userId),
                new KeyValuePair<string, object>("@Status",status)
            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_SetDefaultEmailConfig, param);
        }
    }
}
