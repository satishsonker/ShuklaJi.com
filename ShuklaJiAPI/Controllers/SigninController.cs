using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.BusinessLayer.Common;
using ShuklaJi.ModelLayer.Common;
using ShuklaJi.ModelLayer.Common.ApiResponse;
using ShuklaJi.Utilities;
using ShuklaJiAPI.Areas.Web.Models;
using ShuklaJiAPI.Filter;

namespace ShuklaJiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        [Route("Login")]
        [HttpPost]
        public ApiResponseModel Login(CredentialModel credentialModel)
        {
            SigninDetails signinDetails = new SigninDetails();
            var data = signinDetails.CheckUserCredentials(credentialModel);
            ApiResponseModel apiResponseModel = new ApiResponseModel() {
                Data= data,
                Message=data.Message,
                Status=UtilityConstant.Status.Ok,
                StatusCode=UtilityConstant.StatusCode.Ok
            };

            return apiResponseModel;
        }

        [Authentication]
        [Route("ChangePassword")]
        [HttpPost]
        public ApiResponseModel ChangePassword(ChangePasswordModel changePasswordModel)
        {
            SigninDetails signinDetails = new SigninDetails();
            ApiResponseModel apiResponseModel = new ApiResponseModel()
            {
                Status = UtilityConstant.Status.Ok,
                StatusCode = UtilityConstant.StatusCode.Ok
            };
            apiResponseModel.Message= signinDetails.ChangePassword(changePasswordModel);

            return apiResponseModel;
        }

        [Route("ResetCode")]
        [HttpPost]
        public ApiResponseModel ResetCode([FromQuery] string username, [FromQuery] string appName)
        {
            SigninDetails signinDetails = new SigninDetails();
            ApiResponseModel apiResponseModel = new ApiResponseModel()
            {
                Status = UtilityConstant.Status.Ok,
                StatusCode = UtilityConstant.StatusCode.Ok
            };
            string appUrl=AppSetting.GetAppSettingsData(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.AppUrl, string.Empty);
            apiResponseModel.Message = signinDetails.PasswordResetCode(username, appName, appUrl);

            return apiResponseModel;
        }

        [Route("SetNewPassword")]
        [HttpPost]
        public ApiResponseModel SetNewPassword([FromQuery] string code, [FromQuery] string password)
        {
            SigninDetails signinDetails = new SigninDetails();
            ApiResponseModel apiResponseModel = new ApiResponseModel()
            {
                Status = UtilityConstant.Status.Ok,
                StatusCode = UtilityConstant.StatusCode.Ok
            };
            apiResponseModel.Message = signinDetails.SetNewPassword(code, password);

            return apiResponseModel;
        }
       
    }
}