using ShuklaJi.DataManager;
using ShuklaJi.EmailUtility;
using ShuklaJi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShuklaJi.BusinessLayer.Common
{
    public class EmailServiceDetails
    {
        EmailService emailService = null;
        #region CTOR
        public EmailServiceDetails()
        {
            int _conTimeOut = 300;
            DataExecutor.ConnectionString = Utilities.AppSetting.GetConnectionString(string.Empty, ref _conTimeOut);
            DataExecutor.ConnectionTimeout = _conTimeOut;
            var emailConfig = DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetEmailConfig);
            if (emailConfig != null && emailConfig.Rows.Count > 0)
            {
                EmailConfiguration emailConfiguration = new EmailConfiguration();
                List<EmailConfigList> emailConfigList = emailConfig.ToList<EmailConfigList>();
                var smtpList = emailConfigList.Where(x => x.ConfigName.ToLowerInvariant().Contains("smtp") && x.DefaultConfig).FirstOrDefault();
                var popList = emailConfigList.Where(x => x.ConfigName.ToLowerInvariant().Contains("pop") && x.DefaultConfig).FirstOrDefault();
                if (smtpList != null)
                {
                    emailConfiguration.SmtpPassword = smtpList.Password;
                    emailConfiguration.SmtpPort = smtpList.Port;
                    emailConfiguration.SmtpServer = smtpList.ServerName;
                    emailConfiguration.SmtpUsername = smtpList.Username;
                }

                if (popList != null)
                {
                    emailConfiguration.PopPassword = popList.Password;
                    emailConfiguration.PopPort = popList.Port;
                    emailConfiguration.PopServer = popList.ServerName;
                    emailConfiguration.PopUsername = popList.Username;
                }

                emailService = new EmailService(emailConfiguration);

            }
            else
            {
                throw new Exception("Invalid Email configuration");
            }

        }
        #endregion

        #region Public Methods
        public void Send(EmailMessage emailMessage)
        {
            if (emailService != null)
            {
                emailService.Send(emailMessage);
            }
        }

        public EmailTemplateModel GetEmailTemplate(EmailTemplate emailTemplate)
        {
            EmailTemplateModel emailTemplateModel = new EmailTemplateModel();
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@mappingName",emailTemplate.ToString())
            };
            var dtTemplate = DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetEmailTemplateByMapping, param);
            if (dtTemplate != null && dtTemplate.Rows.Count > 0 && dtTemplate.Columns.Contains("EmailTemplate"))
            {
                emailTemplateModel.Template=dtTemplate.Rows[0]["EmailTemplate"].ToString();
                emailTemplateModel.Subject = dtTemplate.Rows[0]["Subject"].ToString();
            }
            return emailTemplateModel;
        }

        public List<EmailAddress> GetEmailsByGroup(EmailGroup emailGroupName)
        {
            List<EmailAddress> emailAddresses = null;
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@groupname",emailGroupName.ToString())
            };
            var dtTemplate = DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetEmailGroup, param);
            if (dtTemplate != null && dtTemplate.Rows.Count > 0 && dtTemplate.Columns.Contains("Emails"))
            {
                emailAddresses = dtTemplate.Rows[0]["Emails"].ToString().Split(",").Select(x=>new EmailAddress() { Address = x.Trim(), Name = string.Empty }).ToList();
            }
            return emailAddresses;
        }
        #endregion

        #region Enum
        public enum EmailTemplate
        {
            UserCreation,
            ResetPassword,
            ChangePassword,
            NewPassword,
            UserQuery,
            QueryReply
        }

        public enum EmailGroup
        {
            UserQuery
        }
        #endregion
    }
}
