using ShuklaJi.DataManager;
using ShuklaJi.EmailUtility;
using ShuklaJi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuklaJi.BusinessLayer.Areas.Admin.EmailManagement
{
   public class EmailGroupDetails
    {
        #region CTOR
        public EmailGroupDetails()
        {
            int _conTimeOut = 300;
            DataExecutor.ConnectionString = Utilities.AppSetting.GetConnectionString(string.Empty, ref _conTimeOut);
            DataExecutor.ConnectionTimeout = _conTimeOut;
        }
        #endregion
        public List<string> GetEmailsByGroup(string groupName)
        {
            List<string> emails= null;
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@groupname",groupName)
            };
            var dtTemplate = DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetEmailGroup, param);
            if (dtTemplate != null && dtTemplate.Rows.Count > 0 && dtTemplate.Columns.Contains("Emails"))
            {
                emails = dtTemplate.Rows[0]["Emails"].ToString().Split(",").Select(x => x.Trim()).ToList();
            }
            return emails;
        }

        public string UpdateEmailGroup(string groupName,string emails)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@groupname",groupName),
                new KeyValuePair<string, object>("@emails",emails)
            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Usp_Mst_UpdateEmailGroup, param);
        }
    }
}
