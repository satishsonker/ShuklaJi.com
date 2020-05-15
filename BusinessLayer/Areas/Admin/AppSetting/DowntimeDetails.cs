using Microsoft.AspNetCore.Http;
using ShuklaJi.DataManager;
using ShuklaJi.ModelLayer.Areas.Admin.AppSetting;
using ShuklaJi.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static ShuklaJi.Utilities.FileManager;

namespace ShuklaJi.BusinessLayer.Areas.Admin.AppSetting
{
    public class DowntimeDetails
    {
        #region Ctor
        public DowntimeDetails()
        {
            DataExecutor.ConnectionString = Utilities.AppSetting.GetConnectionString(string.Empty);
        }
        #endregion
        public List<DowntimeModel> GetAppDowntime(int DowntimeId=0)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@id",DowntimeId)
            };
            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetAppDowntime, param).ToList<DowntimeModel>();
        }

        public string SaveAppDowntime(DowntimeModel downtimeModel)
        {
            try
            {
                if (downtimeModel != null)
                {
                    List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                        {
                            new KeyValuePair<string, object>("@FromTime",downtimeModel.FromTime),
                            new KeyValuePair<string, object>("@ToTime",downtimeModel.ToTime),
                            new KeyValuePair<string, object>("@UserId",downtimeModel.UserId),
                            new KeyValuePair<string, object>("@Message",downtimeModel.Message)};
                    return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_AddAppDowntime, param);
                }
                else
                {
                    return UtilityConstant.UserResponseStatus.NoDataFromClient;
                }
            }
            catch (Exception ex)
            {
                return UtilityConstant.UserResponseStatus.Error;
            }
        }

        public string UpdateAppDowntime(DowntimeModel downtimeModel)
        {
            try
            {
                if (downtimeModel != null)
                {
                    List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                        {
                            new KeyValuePair<string, object>("@FromTime",downtimeModel.FromTime),
                            new KeyValuePair<string, object>("@ToTime",downtimeModel.ToTime),
                            new KeyValuePair<string, object>("@UserId",downtimeModel.UserId),
                            new KeyValuePair<string, object>("@Id",downtimeModel.Id),
                            new KeyValuePair<string, object>("@Message",downtimeModel.Message)
                    };
                    return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_UpdateAppDowntime, param);
                }
                else
                {
                    return UtilityConstant.UserResponseStatus.NoDataFromClient;
                }
            }
            catch (Exception ex)
            {
                return UtilityConstant.UserResponseStatus.Error;
            }
        }

        public string DeleteAppDowntime(DowntimeModel downtimeModel)
        {
            try
            {
                if (downtimeModel != null)
                {
                    List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                        {
                            new KeyValuePair<string, object>("@UserId",downtimeModel.UserId),
                            new KeyValuePair<string, object>("@Id",downtimeModel.Id)
                    };
                    return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_DeleteAppDowntime, param);
                }
                else
                {
                    return UtilityConstant.UserResponseStatus.NoDataFromClient;
                }
            }
            catch (Exception ex)
            {
                return UtilityConstant.UserResponseStatus.Error;
            }
        }

        public DowntimeModel HasAppDowntime()
        {
            try
            {
                    return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_HasAppDowntime).ToList<DowntimeModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new DowntimeModel();
            }
        }
    }
}
