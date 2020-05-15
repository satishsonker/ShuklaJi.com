using Microsoft.AspNetCore.Mvc;
using ShuklaJi.ModelLayer.Common;
using System;
using System.Collections.Generic;

namespace ShuklaJi.ModelLayer.Web.ContactDetails
{
    public class SendQueryModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Query { get; set; }
        public string AppName { get; set; }
    }

    public class UserQueryModel: SendQueryModel
    {
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int QueryId { get; set; }
    }

    public class ContactDetailModel:CommonPropertyModel
    {
        public string ContactTitle { get; set; }
        public string ContactDescription { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string ConsultationMethods { get; set; }
        public string GoogleLink { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string TwitterLink { get; set; }
        public string YoutubeLink { get; set; }
    }

    public class UserQueryReplyModel : CommonPropertyModel
    {
        public List<string> UserEmail { get; set; }
        public string Reply { get; set; }
        public int QueryId { get; set; }
    }

    public class FeedbackModel:CommonFileUploadModel
    {
        [FromForm(Name ="Id")]
        public int Id { get; set; }

        [FromForm(Name = "Photo")]
        public string Photo { get; set; }

        [FromForm(Name = "Feedback")]
        public string Feedback { get; set; }

        [FromForm(Name = "Source")]
        public string Source { get; set; }

        [FromForm(Name = "Username")]
        public string Username { get; set; }

    }
}
