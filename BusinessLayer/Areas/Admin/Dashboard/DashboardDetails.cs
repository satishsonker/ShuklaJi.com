using ShuklaJi.DataManager;
using ShuklaJi.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ShuklaJi.BusinessLayer.Areas.Admin.Dashboard
{
   public class DashboardDetails
    {
        #region CTOR
        public DashboardDetails()
        {
            DataExecutor.ConnectionString = Utilities.AppSetting.GetConnectionString(Utilities.AppSetting.GetAppSettingsData(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.DefaultConnectionStringName, string.Empty));
        } 
        #endregion

        #region Public Methods
        public DataTable GetDashboardCount()
        {
            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetDashboardCount);
        }
        #endregion
    }
}
