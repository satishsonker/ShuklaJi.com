using System;
using System.Collections.Generic;
using System.Text;
using ShuklaJi.DataManager;
using ShuklaJi.ModelLayer.Areas.Admin.ZodiacManagement;
using ShuklaJi.Utilities;

namespace ShuklaJi.BusinessLayer.Areas.Admin.ZodiacManagement
{
    public class ZodiacDetails
    {
        #region CTOR
        public ZodiacDetails()
        {
            DataExecutor.ConnectionString = Utilities.AppSetting.GetConnectionString(string.Empty);
        }
        #endregion

        #region Public Methods
        public List<ZodiacModel> GetZodiacList()
        {
            return DataExecutor.ExecuteDataTable(Utilities.UtilityConstant.Procedures.Mst_GetZodiac).ToList<ZodiacModel>();
        }

        public string SaveZodiac(ZodiacModel zodiacModel)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
            new KeyValuePair<string, object>("@NameEng",zodiacModel.NameEng),
            new KeyValuePair<string, object>("@NameHindi",zodiacModel.NameHindi),
            new KeyValuePair<string, object>("@UserId",zodiacModel.UserId),
            new KeyValuePair<string, object>("@Logo",zodiacModel.Logo),
            };
            return DataExecutor.ExecuteScalar(Utilities.UtilityConstant.Procedures.Mst_GetZodiac,param);
        }

        public string SaveZodiacReport(ZodiacReportModel zodiacReportModel)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
            new KeyValuePair<string, object>("@ZodiacId",zodiacReportModel.ZodiacId),
            new KeyValuePair<string, object>("@ReportType",zodiacReportModel.ReportType),
            new KeyValuePair<string, object>("@UserId",zodiacReportModel.UserId),
            new KeyValuePair<string, object>("@Description",zodiacReportModel.Description), 
                new KeyValuePair<string, object>("@RangeFrom",zodiacReportModel.RangeFrom),
                new KeyValuePair<string, object>("@RangeTo",zodiacReportModel.RangeTo),
            };
            return DataExecutor.ExecuteScalar(Utilities.UtilityConstant.Procedures.Mst_SaveZodiacReport, param);
        }

        public string UpdateZodiacReport(ZodiacReportModel zodiacReportModel)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
            new KeyValuePair<string, object>("@ZodiacId",zodiacReportModel.ZodiacId),
             new KeyValuePair<string, object>("@ZodiacReportId",zodiacReportModel.ZodiacReportId),
            new KeyValuePair<string, object>("@ReportType",zodiacReportModel.ReportType),
            new KeyValuePair<string, object>("@UserId",zodiacReportModel.UserId),
            new KeyValuePair<string, object>("@Description",zodiacReportModel.Description),
                new KeyValuePair<string, object>("@RangeFrom",zodiacReportModel.RangeFrom),
                new KeyValuePair<string, object>("@RangeTo",zodiacReportModel.RangeTo),
            };
            return DataExecutor.ExecuteScalar(Utilities.UtilityConstant.Procedures.Mst_UpdateZodiacReport, param);
        }

        public List<ZodiacReportModel> GetZodiacReport(int zodiacReportId=0)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
             new KeyValuePair<string, object>("@ZodiacReportId",zodiacReportId)
            };
            return DataExecutor.ExecuteDataTable(Utilities.UtilityConstant.Procedures.Mst_GetZodiacReport, param).ToList<ZodiacReportModel>();
        }

        #endregion

    }
}
