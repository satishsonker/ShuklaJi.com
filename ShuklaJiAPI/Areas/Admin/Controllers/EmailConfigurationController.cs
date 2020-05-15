using Microsoft.AspNetCore.Mvc;
using ShuklaJi.BusinessLayer.Areas.Admin.EmailManagement;
using ShuklaJi.ModelLayer.Areas.Admin.EmailManagement;
using ShuklaJi.ModelLayer.Common.ApiResponse;
using ShuklaJi.Utilities;
using ShuklaJiAPI.Filter;

namespace ShuklaJiAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authentication]
    public class EmailConfigurationController : ControllerBase
    {
        [Route("GetEmailConfig")]
        [HttpGet]
        public ApiResponseModel GetEmailConfig([FromQuery] string configName)
        {
            EmailConfigDetails emailConfigDetails = new EmailConfigDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = emailConfigDetails.GetEmailConfig(configName);
            return response;
        }

        [Route("SaveEmailConfig")]
        [HttpPost]
        public ApiResponseModel GetEmailConfig([FromBody] EmailConfigModel emailConfigModel)
        {
            EmailConfigDetails emailConfigDetails = new EmailConfigDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = emailConfigDetails.SaveEmailConfig(emailConfigModel);
            return response;
        }

        [Route("UpdateEmailConfig")]
        [HttpPost]
        public ApiResponseModel UpdateEmailConfig([FromBody] EmailConfigModel emailConfigModel)
        {
            EmailConfigDetails emailConfigDetails = new EmailConfigDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = emailConfigDetails.UpdateEmailConfig(emailConfigModel);
            return response;
        }

        [Route("DeleteEmailConfig")]
        [HttpPost]
        public ApiResponseModel DeleteEmailConfig([FromQuery] int configId, [FromQuery]  int userId)
        {
            EmailConfigDetails emailConfigDetails = new EmailConfigDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = emailConfigDetails.DeleteEmailConfig(configId, userId);
            return response;
        }

        [Route("SetDefaultEmailConfig")]
        [HttpPost]
        public ApiResponseModel SetDefaultEmailConfig([FromQuery] int configId, [FromQuery]  int userId, [FromQuery]  bool status)
        {
            EmailConfigDetails emailConfigDetails = new EmailConfigDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = emailConfigDetails.SetDefaultEmailConfig(configId, userId, status);
            return response;
        }

        [Route("SaveEmailTemplate")]
        [HttpPost]
        public ApiResponseModel SaveEmailTemplate([FromBody] EmailSaveTemplateModel emailSaveTemplateModel)
        {
            EmailTemplateDetails emailConfigDetails = new EmailTemplateDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = emailConfigDetails.SaveEmailTemplate(emailSaveTemplateModel);
            return response;
        }

        [Route("GetEmailTemplate")]
        [HttpGet]
        public ApiResponseModel GetEmailTemplate([FromQuery] int templateId)
        {
            EmailTemplateDetails emailConfigDetails = new EmailTemplateDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = emailConfigDetails.GetEmailTemplate(templateId);
            return response;
        }
        [Route("GetEmailsByGroup")]
        [HttpGet]
        public ApiResponseModel GetEmailsByGroup([FromQuery] string groupName)
        {
            EmailGroupDetails emailGroupDetails = new EmailGroupDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = emailGroupDetails.GetEmailsByGroup(groupName);
            return response;
        }

        [Route("UpdateEmailGroup")]
        [HttpPost]
        public ApiResponseModel UpdateEmailGroup([FromQuery] string groupName, string emails)
        {
            EmailGroupDetails emailGroupDetails = new EmailGroupDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = emailGroupDetails.UpdateEmailGroup(groupName,emails);
            return response;
        }
    }
}