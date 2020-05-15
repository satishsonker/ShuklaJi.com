using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.BusinessLayer.Web;
using ShuklaJi.ModelLayer.Common.ApiResponse;
using ShuklaJi.ModelLayer.Web;
using ShuklaJi.ModelLayer.Web.ContactDetails;
using ShuklaJi.Utilities;

namespace ShuklaJiAPI.Areas.Web.Controllers
{

    [Area("Web")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        [Route("SendQuery")]
        [HttpPost]
        public ApiResponseModel SendQuery(SendQueryModel sendQueryModel)
        {
            ContactUsDetails contactUsDetails = new ContactUsDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = contactUsDetails.SendQuery(sendQueryModel);
            return response;
        }

        [Route("SaveContactDetails")]
        [HttpPost]
        public ApiResponseModel SaveContactDetails(ContactDetailModel contactDetailModel)
        {
            ContactUsDetails contactUsDetails = new ContactUsDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = contactUsDetails.SaveContactDetails(contactDetailModel);
            return response;
        }

        [Route("GetContactDetails")]
        [HttpGet]
        public ApiResponseModel GetContactDetails()
        {
            ContactUsDetails contactUsDetails = new ContactUsDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = contactUsDetails.GetContactDetails();
            return response;
        }

        [Route("GetUserQuery")]
        [HttpGet]
        public ApiResponseModel GetUserQuery(string status=null)
        {
            ContactUsDetails contactUsDetails = new ContactUsDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = contactUsDetails.GetUserQuery(status);
            return response;
        }

        [Route("ResolveUserQuery")]
        [HttpPost]
        public ApiResponseModel ResolveUserQuery([FromQuery] int queryid)
        {
            ContactUsDetails contactUsDetails = new ContactUsDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = contactUsDetails.ResolveUserQuery(queryid);
            return response;
        }

        [Route("UserQueryReply")]
        [HttpPost]
        public ApiResponseModel UserQueryReply([FromBody] UserQueryReplyModel userQueryReplyModel)
        {
            ContactUsDetails contactUsDetails = new ContactUsDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = contactUsDetails.SaveUserQueryReply(userQueryReplyModel);
            return response;
        }

        [Route("GetUserQueryReply")]
        [HttpGet]
        public ApiResponseModel GetUserQueryReply([FromQuery] int queryId)
        {
            ContactUsDetails contactUsDetails = new ContactUsDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = contactUsDetails.GetUserQueryReply(queryId);
            return response;
        }

        [Route("SaveFeedback")]
        [HttpPost]
        public ApiResponseModel SaveFeedback([FromForm] FeedbackModel feedbackModel)
        {
            ContactUsDetails contactUsDetails = new ContactUsDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = contactUsDetails.SaveFeedback(feedbackModel);
            return response;
        }

        [Route("UpdateFeedback")]
        [HttpPost]
        public ApiResponseModel UpdateFeedback([FromForm] FeedbackModel feedbackModel)
        {
            ContactUsDetails contactUsDetails = new ContactUsDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = contactUsDetails.UpdateFeedback(feedbackModel);
            return response;
        }

        [Route("DeleteFeedback")]
        [HttpPost]
        public ApiResponseModel DeleteFeedback([FromForm] FeedbackModel feedbackModel)
        {
            ContactUsDetails contactUsDetails = new ContactUsDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = contactUsDetails.UpdateFeedback(feedbackModel);
            return response;
        }

        [Route("GetFeedback")]
        [HttpGet]
        public ApiResponseModel GetFeedback()
        {
            ContactUsDetails contactUsDetails = new ContactUsDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = contactUsDetails.GetFeedback();
            return response;
        }
    }
}