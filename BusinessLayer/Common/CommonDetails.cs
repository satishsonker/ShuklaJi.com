using ModelLayer.Common.ApiResponse;
using ShuklaJi.DataManager;
using ShuklaJi.Utilities;
using System.Collections.Generic;
using System.Data;
using static ShuklaJi.Utilities.FileManager;

namespace ShuklaJi.BusinessLayer.Common
{
    public class CommonDetails
    {
        #region Ctor
        public CommonDetails()
        {
            DataExecutor.ConnectionString = AppSetting.GetConnectionString(string.Empty);
        }
        #endregion

        public bool IsDataExists(string key, string value)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                new KeyValuePair<string, object>("@key",key),
                new KeyValuePair<string, object>("@value",value)
            };
            var result = DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_IsDataExists, param);
            bool.TryParse(result, out bool isExists);
            return isExists;
        }

        public DataTable DropdownList(string key)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                new KeyValuePair<string, object>("@key",key)
            };
            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_DropdownList, param);
        }

        //public FileResponseModel FileUpload(FileUploadModel fileUploadModel)
        //{
        //    if(fileUploadModel!=null && fileUploadModel.File != null)
        //    {
        //        var fileModels = new List<FileModel>();
        //        string imgData = string.Empty;
        //        if ()
        //        {
        //            fileModels = GetFileData(new List<IFormFile>() { addUserModel.File }, FileType.Image);

        //            if (fileModels.Count > 0)
        //            {
        //                imgData = fileModels[0].DataBase64;
        //            }
        //        }
        //    }
        //}
    }
}
