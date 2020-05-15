using Microsoft.AspNetCore.Http;
using ShuklaJi.BusinessLayer.Common;
using ShuklaJi.DataManager;
using ShuklaJi.EmailUtility;
using ShuklaJi.ModelLayer.Web.ContactDetails;
using ShuklaJi.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using static ShuklaJi.BusinessLayer.Common.EmailServiceDetails;
using static ShuklaJi.Utilities.FileManager;

namespace ShuklaJi.BusinessLayer.Web
{
    public class ContactUsDetails
    {
        #region CTOR
        public ContactUsDetails()
        {
            DataExecutor.ConnectionString = Utilities.AppSetting.GetConnectionString(string.Empty);
        }
        #endregion
        public string SendQuery(SendQueryModel sendQueryModel)
        {
            EmailServiceDetails emailServiceDetails = new EmailServiceDetails();
            EmailMessage emailMessage = new EmailMessage();
            var emailTemplate = emailServiceDetails.GetEmailTemplate(EmailTemplate.UserQuery);
            emailMessage.ToAddresses = emailServiceDetails.GetEmailsByGroup(EmailGroup.UserQuery);
            if (emailTemplate != null && sendQueryModel != null)
            {
                try
                {
                    emailMessage.Subject = emailTemplate.Subject.Replace("#AppName#", sendQueryModel.AppName);
                    emailMessage.Content = emailTemplate.Template;
                    emailMessage.Content = emailMessage.Content.Replace("#Body#", sendQueryModel.Query);
                    emailMessage.Content = emailMessage.Content.Replace("#AppName#", sendQueryModel.AppName);
                    emailMessage.Content = emailMessage.Content.Replace("#Name#", sendQueryModel.Name);
                    emailMessage.Content = emailMessage.Content.Replace("#Mobile#", sendQueryModel.Mobile);
                    emailMessage.Content = emailMessage.Content.Replace("#Email#", sendQueryModel.Email);

                    List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                new KeyValuePair<string, object>("@Name",sendQueryModel.Name),
                new KeyValuePair<string, object>("@Query",sendQueryModel.Query),
                new KeyValuePair<string, object>("@Mobile",sendQueryModel.Mobile),
                new KeyValuePair<string, object>("@Email",sendQueryModel.Email) };
                    var data = DataExecutor.ExecuteNonQuery(UtilityConstant.Procedures.Mst_SaveUserQuery, param);

                    Thread sendmail = new Thread(() => { emailServiceDetails.Send(emailMessage); });
                    sendmail.Start();
                }
                catch (Exception ex)
                {
                    return UtilityConstant.UserResponseStatus.Error;
                }
            }
            return "EmailSend";
        }

        public string SaveContactDetails(ContactDetailModel contactDetailModel)
        {
            if (contactDetailModel != null)
            {
                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                new KeyValuePair<string, object>("@ContactTitle",contactDetailModel.ContactTitle),
                new KeyValuePair<string, object>("@ContactDescription",contactDetailModel.ContactDescription),
                new KeyValuePair<string, object>("@Address",contactDetailModel.Address),
                new KeyValuePair<string, object>("@Mobile",contactDetailModel.Mobile),
                new KeyValuePair<string, object>("@Email",contactDetailModel.Email),
                new KeyValuePair<string, object>("@ConsultationMethods",contactDetailModel.ConsultationMethods),
                new KeyValuePair<string, object>("@UserId",contactDetailModel.UserId),

                new KeyValuePair<string, object>("@GoogleLink",contactDetailModel.GoogleLink),
                new KeyValuePair<string, object>("@FacebookLink",contactDetailModel.FacebookLink),
                new KeyValuePair<string, object>("@TwitterLink",contactDetailModel.TwitterLink),
                new KeyValuePair<string, object>("@InstagramLink",contactDetailModel.InstagramLink),
                new KeyValuePair<string, object>("@YoutubeLink",contactDetailModel.YoutubeLink)
                };

                return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_SaveContactDetails, param);
            }
            return UtilityConstant.UserResponseStatus.Error;
        }

        public ContactDetailModel GetContactDetails()
        {
            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetContactDetails).ToList<ContactDetailModel>().FirstOrDefault();
        }

        public List<UserQueryModel> GetUserQuery(string status = null)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                new KeyValuePair<string, object>("@status",status) };
            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetUserQuery, param).ToList<UserQueryModel>();
        }

        public string ResolveUserQuery(int queryId)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                new KeyValuePair<string, object>("@queryId",queryId) };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_ResolveUserQuery, param);
        }

        public string SaveUserQueryReply(UserQueryReplyModel userQueryReplyModel)
        {

            EmailServiceDetails emailServiceDetails = new EmailServiceDetails();
            EmailMessage emailMessage = new EmailMessage();
            var emailTemplate = emailServiceDetails.GetEmailTemplate(EmailTemplate.QueryReply);

            if (emailTemplate != null && userQueryReplyModel != null)
            {
                try
                {
                    emailMessage.ToAddresses = userQueryReplyModel.UserEmail.Select(x => new EmailAddress() { Address = x, Name = string.Empty }).ToList();
                    emailMessage.Subject = emailTemplate.Subject.Replace("#AppName#", userQueryReplyModel.AppName);
                    emailMessage.Content = emailTemplate.Template;
                    emailMessage.Content = emailMessage.Content.Replace("#Body#", userQueryReplyModel.Reply);

                    List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                        new KeyValuePair<string, object>("@QueryId",userQueryReplyModel.QueryId),
                        new KeyValuePair<string, object>("@Reply",userQueryReplyModel.Reply),
                        new KeyValuePair<string, object>("@UserId",userQueryReplyModel.UserId),
                        new KeyValuePair<string, object>("@SentFrom",userQueryReplyModel.UserEmail[0]),
                        new KeyValuePair<string, object>("@SentTo",string.Join(",",userQueryReplyModel.UserEmail)),
                        new KeyValuePair<string, object>("@Subject",emailMessage.Subject)
                        };
                    DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_SaveUserQueryReply, param);

                    Thread sendmail = new Thread(() => { emailServiceDetails.Send(emailMessage); });
                    sendmail.Start();
                }
                catch (Exception ex)
                {
                    return UtilityConstant.UserResponseStatus.Error;
                }
            }

            return "ReplySend";
        }

        public DataSet GetUserQueryReply(int queryId)
        {
            try
            {
                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                        new KeyValuePair<string, object>("@QueryId",queryId)
                        };
                return DataExecutor.ExecuteDataSet(UtilityConstant.Procedures.Mst_GetUserQueyReply, param);
            }
            catch (Exception ex)
            {
                return new DataSet();
            }
        }

        public List<FeedbackModel> GetFeedback()
        {
            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetFeedback).ToList<FeedbackModel>();
        }

        public string SaveFeedback(FeedbackModel feedbackModel)
        {
            if (feedbackModel != null)
            {
               
                string imgData = string.Empty;
                if (feedbackModel.File != null)
                {
                    List<FileModel> fileModels = GetFileData(new List<IFormFile>() { feedbackModel.File }, FileType.Image);

                    if (fileModels.Count > 0)
                    {
                        imgData = fileModels[0].DataBase64;
                    }
                }
                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                new KeyValuePair<string, object>("@feedback",feedbackModel.Feedback),
                 new KeyValuePair<string, object>("@photo",imgData.IndexOf("data:image/png;base64, ")>-1?imgData:"data:image/png;base64, "+imgData),
                new KeyValuePair<string, object>("@username",feedbackModel.Username),
                new KeyValuePair<string, object>("@source",feedbackModel.Source),
                new KeyValuePair<string, object>("@UserId",feedbackModel.UserId)
                };

                return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_SaveFeedback, param);
            }
            return UtilityConstant.UserResponseStatus.Error;
        }

        public string UpdateFeedback(FeedbackModel feedbackModel)
        {
            if (feedbackModel != null)
            {

                string imgData = string.Empty;
                if (feedbackModel.File != null)
                {
                    List<FileModel> fileModels = GetFileData(new List<IFormFile>() { feedbackModel.File }, FileType.Image);

                    if (fileModels.Count > 0)
                    {
                        imgData = fileModels[0].DataBase64;
                    }
                }
                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                    new KeyValuePair<string, object>("@id",feedbackModel.Id),
                new KeyValuePair<string, object>("@feedback",feedbackModel.Feedback),
                 new KeyValuePair<string, object>("@photo",imgData.IndexOf("data:image/png;base64, ")>-1?imgData:"data:image/png;base64, "+imgData),
                new KeyValuePair<string, object>("@username",feedbackModel.Username),
                new KeyValuePair<string, object>("@source",feedbackModel.Source),
                new KeyValuePair<string, object>("@UserId",feedbackModel.UserId)
                };

                return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_UpdateFeedback, param);
            }
            return UtilityConstant.UserResponseStatus.Error;
        }

        public string DeleteFeedback(FeedbackModel feedbackModel)
        {
            if (feedbackModel != null)
            {
                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                     new KeyValuePair<string, object>("@id",feedbackModel.Id),
                new KeyValuePair<string, object>("@feedback",feedbackModel.Feedback),
                new KeyValuePair<string, object>("@ContactDescription",feedbackModel.Photo),
                new KeyValuePair<string, object>("@Address",feedbackModel.Source),
                new KeyValuePair<string, object>("@UserId",feedbackModel.UserId)
                };

                return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_DeleteFeedback, param);
            }
            return UtilityConstant.UserResponseStatus.Error;
        }
    }
}
