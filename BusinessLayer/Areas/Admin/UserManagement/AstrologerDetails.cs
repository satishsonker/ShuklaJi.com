using Microsoft.AspNetCore.Http;
using ShuklaJi.DataManager;
using ShuklaJi.ModelLayer.Areas.Admin.UserManagement;
using ShuklaJi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ShuklaJi.Utilities.FileManager;

namespace ShuklaJi.BusinessLayer.Areas.Admin.UserManagement
{
    public class AstrologerDetails
    {
        #region CTOR
        public AstrologerDetails()
        {
            DataExecutor.ConnectionString = Utilities.AppSetting.GetConnectionString(string.Empty);
        }
        #endregion

        #region Public Method
        public List<AstrologerModel> GetAstrologers(int astrologerId=0)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@astrologerId",astrologerId)
            };

            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetAstrologers, param).ToList<AstrologerModel>();
        }

        public string SaveAstrologers(AstrologerModel model,string rootPath)
        {
            if (model != null)
            {
                FileManager fileManager = new FileManager();
                var fileresponse=fileManager.SaveFile(model.File, rootPath);
                var fileModels = new List<FileModel>();
                if (model.File != null)
                {
                    fileModels = GetFileData(new List<IFormFile>() { model.File }, FileType.Image);
                }
                string imgData = string.Empty;
                if (fileModels.Count > 0)
                {
                    imgData = fileModels[0].DataBase64;
                }
                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@Address",model.Address),
                new KeyValuePair<string, object>("@ConsultOn",model.ConsultOn),
                new KeyValuePair<string, object>("@ConsultPrice",model.ConsultPrice),
                new KeyValuePair<string, object>("@Dob",model.Dob),
                new KeyValuePair<string, object>("@Email",model.Email),
                new KeyValuePair<string, object>("@Experience",model.Experience),
                new KeyValuePair<string, object>("@Language",model.Language),
                new KeyValuePair<string, object>("@Location",model.Location),
                new KeyValuePair<string, object>("@Mobile",model.Mobile),
                new KeyValuePair<string, object>("@Name",model.Name),
                   new KeyValuePair<string, object>("@Photo",fileresponse.FileFullpath),
                new KeyValuePair<string, object>("@userId",model.UserId),
                new KeyValuePair<string, object>("@biography",model.Biography),
                new KeyValuePair<string, object>("@Experties",model.Experties),
            };

                return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_SaveAstrologers, param);
            }
            return "Error";
        }

        public string UpdateAstrologers(AstrologerModel model,string rootPath)
        {
            var fileModels = new List<FileModel>();
            if (model != null)
            {
                FileManager fileManager = new FileManager();
                var fileresponse = fileManager.SaveFile(model.File, rootPath);
                if (model.File != null)
                {
                    fileModels = GetFileData(new List<IFormFile>() { model.File }, FileType.Image);
                }
                string imgData = string.Empty;
                if (fileModels.Count > 0)
                {
                    imgData = fileModels[0].DataBase64;
                }
                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                {
                new KeyValuePair<string, object>("@Address",model.Address),
                new KeyValuePair<string, object>("@ConsultOn",model.ConsultOn),
                new KeyValuePair<string, object>("@ConsultPrice",model.ConsultPrice),
                new KeyValuePair<string, object>("@Dob",model.Dob),
                new KeyValuePair<string, object>("@Email",model.Email),
                new KeyValuePair<string, object>("@Experience",model.Experience),
                new KeyValuePair<string, object>("@Language",model.Language),
                new KeyValuePair<string, object>("@Location",model.Location),
                new KeyValuePair<string, object>("@Mobile",model.Mobile),
                new KeyValuePair<string, object>("@Name",model.Name),
                 new KeyValuePair<string, object>("@Photo",fileresponse.FileFullpath),
                new KeyValuePair<string, object>("@userId",model.UserId),
                 new KeyValuePair<string, object>("@astrologerId",model.AstrologerId),
                new KeyValuePair<string, object>("@biography",model.Biography),
                new KeyValuePair<string, object>("@Experties",model.Experties),
            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_UpdateAstrologers, param);
            }

            return "Error";

        }

        public string DeleteAstrologers(AstrologerModel model)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@userId",model.UserId),
                 new KeyValuePair<string, object>("@astrologerId",model.AstrologerId),
            };

            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_DeleteAstrologers, param);
        }

        public List<AstrologerScheduleModel> GetAstrologerSchedule(int astrologerId)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                 new KeyValuePair<string, object>("@astrologerId",astrologerId),
            };

            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetAstrologerSchedule, param).ToList<AstrologerScheduleModel>();
        }

        public string SaveAstrologerSchedule(List<AstrologerScheduleModel> model)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                   new KeyValuePair<string, object>("@db",model.ToDataTable())
            };

            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_SaveAstrologerSchedule, param);
        }
        public string UpdateAstrologerSchedule(AstrologerScheduleModel model)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                 new KeyValuePair<string, object>("@astrologerId",model.AstrologerId),
                  new KeyValuePair<string, object>("@ScheduleId",model.ScheduleId),
                 new KeyValuePair<string, object>("@Day",model.Day),
                 new KeyValuePair<string, object>("@FromTime",model.FromTime),
                 new KeyValuePair<string, object>("@ToTime",model.ToTime),
                 new KeyValuePair<string, object>("@Remark",model.Remarks),
                 new KeyValuePair<string, object>("@Is24hours",model.Is24hours), 
                new KeyValuePair<string, object>("@userId",model.UserId),
            };

            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_UpdateAstrologerSchedule, param);
        }

        public string DeleteAstrologerSchedule(AstrologerScheduleModel model)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                 new KeyValuePair<string, object>("@astrologerId",model.AstrologerId),
                  new KeyValuePair<string, object>("@ScheduleId",model.ScheduleId),
                new KeyValuePair<string, object>("@userId",model.UserId),
            };

            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_UpdateAstrologerSchedule, param);
        }

        public string SaveAstrologerRating(AstrologerRatingModel astrologerRatingModel)
                {
            try
            {
                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                    {
                         new KeyValuePair<string, object>("@astrologerId",astrologerRatingModel.AstrologerId),
                          new KeyValuePair<string, object>("@star",astrologerRatingModel.Stars),
                           new KeyValuePair<string, object>("@review",astrologerRatingModel.Review),
                            new KeyValuePair<string, object>("@userId",astrologerRatingModel.UserId),
                    };

                return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_SaveAstroRating, param);
            }
            catch (Exception ex)
            {

                throw;
            }
                }
        public List<AstrologerRatingModel> GetAstrologerRating(int astrologerId)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                 new KeyValuePair<string, object>("@astrologerId",astrologerId),
            };

            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetAstrologerRating, param).ToList<AstrologerRatingModel>();
        }
        #endregion
    }
}
