using Microsoft.AspNetCore.Http;
using ModelLayer.Common.ApiResponse;
using ShuklaJi.BusinessLayer.Common;
using ShuklaJi.DataManager;
using ShuklaJi.EmailUtility;
using ShuklaJi.ModelLayer.Areas.Admin.UserManagement;
using ShuklaJi.ModelLayer.Common;
using ShuklaJi.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static ShuklaJi.BusinessLayer.Common.EmailServiceDetails;
using static ShuklaJi.Utilities.FileManager;

namespace ShuklaJi.BusinessLayer.Areas.Admin.UserManagement
{
    public class UserDetails
    {
        private string _rootPath = string.Empty;
        #region CTOR
        public UserDetails(string rootPath)
        {
            DataExecutor.ConnectionString = Utilities.AppSetting.GetConnectionString(string.Empty);
            _rootPath = rootPath;
        }
        #endregion

        #region Public Method
        public string AddUser(UserModel addUserModel)
        {
            try
            {
                if (addUserModel != null)
                {
                    FileManager fileManager = new FileManager();
                    FileResponseModel fileResponseModel = null;
                    if (addUserModel.File != null)
                    {
                        fileResponseModel= fileManager.SaveFile(addUserModel.File, _rootPath);
                    }

                    List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string, object>("@userName",addUserModel.UserName),
                        new KeyValuePair<string, object>("@roleId",addUserModel.RoleId),
                        new KeyValuePair<string, object>("@firstName",addUserModel.FirstName),
                        new KeyValuePair<string, object>("@lastName",addUserModel.LastName),
                         new KeyValuePair<string, object>("@email",addUserModel.Email),
                        new KeyValuePair<string, object>("@mobile",addUserModel.Mobile),
                        new KeyValuePair<string, object>("@dob",addUserModel.Dob),
                        new KeyValuePair<string, object>("@gender",addUserModel.Gender),
                        new KeyValuePair<string, object>("@createdBy",addUserModel.CreatedBy),
                        new KeyValuePair<string, object>("@photo",fileResponseModel?.FileFullpath??string.Empty)

                    };
                    List<string> tolist = new List<string>() { addUserModel.Email };
                    return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_AddUser, param);
                }
                return UtilityConstant.UserResponseStatus.NoDataFromClient;

            }
            catch (Exception ex)
            {
                return UtilityConstant.UserResponseStatus.Error;
            }

        }

        public async Task<LoginTokenDataModel> AddExternalUser(ExternalUserModel addUserModel)
        {
            try
            {
                if (addUserModel != null)
                {   
                    List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string, object>("@dob",addUserModel.Dob),
                        new KeyValuePair<string, object>("@gender",addUserModel.Gender),
                        new KeyValuePair<string, object>("@firstName",addUserModel.FirstName),
                        new KeyValuePair<string, object>("@lastName",addUserModel.LastName),
                         new KeyValuePair<string, object>("@email",addUserModel.Email),
                        new KeyValuePair<string, object>("@mobile",addUserModel.Mobile),
                        new KeyValuePair<string, object>("@Provider",addUserModel.Provider),
                        new KeyValuePair<string, object>("@ProviderKey",addUserModel.ProviderKey)
                    };
                    SigninDetails signinDetails = new SigninDetails();
                    CredentialModel credentialModel = new CredentialModel() { 
                    isExternal=true,UserName=addUserModel.Email
                    };

                    
                     DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_AddExternalUser, param);

                    return signinDetails.CheckUserCredentials(credentialModel);
                }
                return new LoginTokenDataModel();

            }
            catch (Exception ex)
            {
                return new LoginTokenDataModel();
            }

        }

        public DataTable GetUser(int userId = 0)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@UserId",userId)
            };
            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetUsers, param);
        }

        public string UpdateUser(UserModel addUserModel)
        {
            if (addUserModel != null)
            {
                if (addUserModel != null)
                {
                    FileManager fileManager = new FileManager();
                    var filemodel = fileManager.SaveFile(addUserModel.File, _rootPath);
                    List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string, object>("@userName",addUserModel.UserName),
                        new KeyValuePair<string, object>("@roleId",addUserModel.RoleId),
                        new KeyValuePair<string, object>("@firstName",addUserModel.FirstName),
                        new KeyValuePair<string, object>("@lastName",addUserModel.LastName),
                        new KeyValuePair<string, object>("@createdBy",addUserModel.CreatedBy),
                        new KeyValuePair<string, object>("@email",addUserModel.Email),
                        new KeyValuePair<string, object>("@mobile",addUserModel.Mobile),
                        new KeyValuePair<string, object>("@userId",addUserModel.UserId),

                        new KeyValuePair<string, object>("@dob",addUserModel.Dob),
                        new KeyValuePair<string, object>("@gender",addUserModel.Gender),
                        new KeyValuePair<string, object>("@photo",filemodel?.FileFullpath??string.Empty),
                    };
                    List<string> tolist = new List<string>() { addUserModel.Email };
                    SendUserCreationMail(tolist, $"{addUserModel.FirstName} {addUserModel.LastName}", addUserModel.AppName);
                    return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_UpdateUser, param);
                }
            }
            return UtilityConstant.UserResponseStatus.Error;
        }

        public string DeleteUser(int modifiedBy, int userId)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@userId",userId),
                new KeyValuePair<string, object>("@modifiedBy",modifiedBy)
            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_DeleteUser, param);
        }

        public string ActiveDeactiveUser(int modifiedBy, int userId, bool status)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@userId",userId),
                new KeyValuePair<string, object>("@modifiedBy",modifiedBy),
                new KeyValuePair<string, object>("@status",status)
            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_ActiveDeactiveUser, param);
        }

        private void SendUserCreationMail(List<string> toList, string username, string appName)
        {
            EmailServiceDetails emailServiceDetails = new EmailServiceDetails();
            EmailMessage emailMessage = new EmailMessage();
            var emailTemplate = emailServiceDetails.GetEmailTemplate(EmailTemplate.UserCreation);
            if (emailTemplate != null)
            {
                try
                {
                    emailMessage.ToAddresses = toList.Select(x => new EmailAddress() { Address = x, Name = string.Empty }).ToList();
                    emailMessage.Subject = emailTemplate.Subject;
                    emailMessage.Content = emailTemplate.Template;
                    emailMessage.Content = emailMessage.Content.Replace("#Name#", username);
                    emailMessage.Content = emailMessage.Content.Replace("#AppName#", appName);

                    emailServiceDetails.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion
    }
}
