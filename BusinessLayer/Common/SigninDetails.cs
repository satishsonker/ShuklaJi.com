using Newtonsoft.Json;
using ShuklaJi.DataManager;
using ShuklaJi.EmailUtility;
using ShuklaJi.ModelLayer.Common;
using ShuklaJi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShuklaJi.BusinessLayer.Common.EmailServiceDetails;

namespace ShuklaJi.BusinessLayer.Common
{
    public class SigninDetails
    {
        #region Ctor
        public SigninDetails()
        {
            DataExecutor.ConnectionString = AppSetting.GetConnectionString(string.Empty);
        }
        #endregion

        #region Public Methods
        public LoginTokenDataModel CheckUserCredentials(CredentialModel credentialModel)
        {
            try
            {
                LoginTokenDataModel loginTokenDataModel = new LoginTokenDataModel();
                if (credentialModel != null)
                {
                    List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@username",credentialModel.UserName),
                        new KeyValuePair<string, object>("@isExternal",credentialModel.isExternal),
                    new KeyValuePair<string, object>("@password",EncryptManager.GetHashString(credentialModel.Password??string.Empty))
                };
                    var data = DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Usp_CheckUserCredential, param);
                    if (data != null && data.Columns.Count > 1)
                    {
                        LoginTokenModel loginTokenModel = new LoginTokenModel()
                        {
                            ExpireTime = DateTime.Now.AddMinutes(30),
                            Message = string.Empty,
                            SecureId = Guid.NewGuid().ToString(),
                            UserData = data
                        };
                        loginTokenDataModel.UserData = data;
                        loginTokenDataModel.Message = "ValidUser";
                        loginTokenDataModel.Token = EncryptManager.Encrypt(JsonConvert.SerializeObject(loginTokenModel), AppSetting.GetAppSettingsData(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.SaltKey, string.Empty));
                    }
                    else
                    {
                        loginTokenDataModel.Message = data.Rows[0][0].ToString();
                    }

                }
                return loginTokenDataModel;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<LoginTokenDataModel> CheckUserCredentialsAsync(CredentialModel credentialModel)
        {
            try
            {
                LoginTokenDataModel loginTokenDataModel = new LoginTokenDataModel();
                if (credentialModel != null)
                {
                    List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@username",credentialModel.UserName),
                    new KeyValuePair<string, object>("@password",EncryptManager.GetHashString(credentialModel.Password??string.Empty))
                };
                    var data = DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Usp_CheckUserCredential, param);
                    if (data != null && data.Columns.Count > 1)
                    {
                        LoginTokenModel loginTokenModel = new LoginTokenModel()
                        {
                            ExpireTime = DateTime.Now.AddMinutes(30),
                            Message = string.Empty,
                            SecureId = Guid.NewGuid().ToString(),
                            UserData = data
                        };
                        loginTokenDataModel.UserData = data;
                        loginTokenDataModel.Message = "ValidUser";
                        loginTokenDataModel.Token = EncryptManager.Encrypt(JsonConvert.SerializeObject(loginTokenModel), AppSetting.GetAppSettingsData(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.SaltKey, string.Empty));
                    }
                    else
                    {
                        loginTokenDataModel.Message = data.Rows[0][0].ToString();
                    }

                }
                return loginTokenDataModel;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public string ChangePassword(ChangePasswordModel changePasswordModel)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@username",changePasswordModel.UserName),
                    new KeyValuePair<string, object>("@oldpassword",EncryptManager.GetHashString(changePasswordModel.OldPassword??string.Empty)),
                     new KeyValuePair<string, object>("@newpassword",EncryptManager.GetHashString(changePasswordModel.NewPassword??string.Empty))
                };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Usp_ChangePassword, param);
        }

        public string PasswordResetCode(string username,string appName,string appUrl)
        {
            string code = EncryptManager.GetHashString($"{username}-{DateTime.Now.ToShortDateString()}");
            string link = $"{appUrl}common/signin/newpassword?txn={code}";
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@username",username),
                    new KeyValuePair<string, object>("@code",code)
                };
            var result = DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Usp_AddResetCode, param);
            if (result != null && result.Rows.Count > 0)
            {
                if (result.Columns.Count > 1)
                {
                    PasswordResetLinkMail(new List<string>() { result.Rows[0]["EmailId"].ToString() }, result.Rows[0]["Name"].ToString(), link, result.Rows[0]["ResetCodeExpire"].ToString(), appName);
                }
                return result.Rows[0]["Msg"].ToString();
            }
            return UtilityConstant.UserResponseStatus.Error;
        }

        public string SetNewPassword(string code, string password)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@code",code),
                    new KeyValuePair<string, object>("@newPassword",EncryptManager.GetHashString(password??string.Empty))
                };
            var result = DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Usp_SetNewPassword, param);
            if (result != null && result.Rows.Count > 0)
            {
                if (result.Columns.Count > 1)
                {
                    NewPasswordMail(new List<string>() { result.Rows[0]["EmailId"].ToString() }, result.Rows[0]["Name"].ToString(), result.Rows[0]["AppName"].ToString());
                }
                return result.Rows[0]["Msg"].ToString();
            }
            return UtilityConstant.UserResponseStatus.Error;
        }

        private void NewPasswordMail(List<string> toList, string name, string appName)
        {
            EmailServiceDetails emailServiceDetails = new EmailServiceDetails();
            EmailMessage emailMessage = new EmailMessage();
            var emailTemplate = emailServiceDetails.GetEmailTemplate(EmailTemplate.NewPassword);
            if (emailTemplate != null)
            {
                try
                {
                    emailMessage.ToAddresses = toList.Select(x => new EmailAddress() { Address = x, Name = string.Empty }).ToList();
                    emailMessage.Subject = emailTemplate.Subject;
                    emailMessage.Content = emailTemplate.Template;
                    emailMessage.Content = emailMessage.Content.Replace("#Name#", name);
                    emailMessage.Content = emailMessage.Content.Replace("#AppName#", appName);
                    emailServiceDetails.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private void PasswordResetLinkMail(List<string> toList, string username, string link, string expireTime,string appName)
        {
            EmailServiceDetails emailServiceDetails = new EmailServiceDetails();
            EmailMessage emailMessage = new EmailMessage();
            var emailTemplate = emailServiceDetails.GetEmailTemplate(EmailTemplate.ResetPassword);
            if (emailTemplate != null)
            {
                try
                {
                    emailMessage.ToAddresses = toList.Select(x => new EmailAddress() { Address = x, Name = string.Empty }).ToList();
                    emailMessage.Subject = emailTemplate.Subject;
                    emailMessage.Content = emailTemplate.Template;
                    emailMessage.Content = emailMessage.Content.Replace("#link#", link);
                    emailMessage.Content = emailMessage.Content.Replace("#Name#", username);
                    emailMessage.Content = emailMessage.Content.Replace("#Expire#", expireTime);
                    emailMessage.Content = emailMessage.Content.Replace("#AppName#", appName);
                    emailServiceDetails.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        #endregion
    }
}
