using Microsoft.AspNetCore.Http;
using ShuklaJi.DataManager;
using ShuklaJi.ModelLayer.Areas.Admin.AppSetting;
using ShuklaJi.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using static ShuklaJi.Utilities.FileManager;

namespace ShuklaJi.BusinessLayer.Areas.Admin.AppSetting
{
    public class AppSettingDetails
    {
        #region Ctor
        public AppSettingDetails()
        {
            DataExecutor.ConnectionString = Utilities.AppSetting.GetConnectionString(string.Empty);
        }
        #endregion
        public DataTable GetAppSetting()
        {
            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetAppSetting);
        }

        public string SetAppSetting(AppSettingModel appSettingData)
        {
            try
            {
                var fileModels = new List<FileModel>();
                if (appSettingData != null)
                {
                    if (appSettingData.File != null)
                    {
                        fileModels = GetFileData(new List<IFormFile>() { appSettingData.File }, FileType.Image);
                    }
                    string imgData = appSettingData.Image;
                    if (fileModels.Count > 0)
                    {
                        imgData = fileModels[0].DataBase64;
                    }
                    if (fileModels != null)
                    {
                        List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                        {
                            new KeyValuePair<string, object>("@AppLogo",imgData.Replace(@"data:image/png;base64, ","")),
                            new KeyValuePair<string, object>("@AppName",appSettingData.AppName??"श्री आदित्य ज्योतिष केंद्र"),
                            new KeyValuePair<string, object>("@UserId",appSettingData.UserId),
                            new KeyValuePair<string, object>("@DefaultPassword",appSettingData.DefaultPassword),
                            new KeyValuePair<string, object>("@ResetLinkValidity",appSettingData.ResetLinkValidity),
                            new KeyValuePair<string, object>("@DefaultPasswordHash",EncryptManager.GetHashString(appSettingData.DefaultPassword)),
                        };
                        return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_UpdateAppSetting, param);
                    }
                    else
                    {
                        return UtilityConstant.UserResponseStatus.NoDataFromClient;
                    }
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
    }
}
