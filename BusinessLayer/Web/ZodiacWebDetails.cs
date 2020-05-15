using ShuklaJi.DataManager;
using ShuklaJi.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Web
{
   public class ZodiacWebDetails
    {
        #region CTOR
        public ZodiacWebDetails()
        {
            DataExecutor.ConnectionString = AppSetting.GetConnectionString(string.Empty);
        }

        public object GetHoroscope(string reportType,DateTime date,int id)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@reportType",reportType),
                new KeyValuePair<string, object>("@date",date),
                new KeyValuePair<string, object>("@zodiacId",id)
            };
            return DataExecutor.ExecuteDataSet(UtilityConstant.Procedures.Mst_GetHoroscope, param);
        }
        #endregion
    }
}
