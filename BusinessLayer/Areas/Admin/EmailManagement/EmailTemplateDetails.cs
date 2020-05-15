
using ShuklaJi.DataManager;
using ShuklaJi.ModelLayer.Areas.Admin.EmailManagement;
using ShuklaJi.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace ShuklaJi.BusinessLayer.Areas.Admin.EmailManagement
{
    public class EmailTemplateDetails
    {
        #region COTR
        public EmailTemplateDetails()
        {
            DataExecutor.ConnectionString = Utilities.AppSetting.GetConnectionString(Utilities.AppSetting.GetAppSettingsData(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.DefaultConnectionStringName, string.Empty));
        }
        #endregion

        #region Public Methods
        public string SaveEmailTemplate(EmailSaveTemplateModel emailSaveTemplateModel)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                new KeyValuePair<string, object>("@userId",emailSaveTemplateModel.UserId),
                new KeyValuePair<string, object>("@EmailTemplate",emailSaveTemplateModel.EmailTemplate),
                new KeyValuePair<string, object>("@EmailTemplateName",emailSaveTemplateModel.EmailTemplateName),
                new KeyValuePair<string, object>("@Subject",emailSaveTemplateModel.Subject),
            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_AddEmailTemplate, param);
        }

        public EmailSaveTemplateModel GetEmailTemplate(int templateId)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                new KeyValuePair<string, object>("@templateId",templateId)
            };
            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetEmailTemplateById, param).ToList<EmailSaveTemplateModel>().FirstOrDefault();
        }
        #endregion
    }
}
