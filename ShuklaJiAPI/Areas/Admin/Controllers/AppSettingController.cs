using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.BusinessLayer.Areas.Admin.AppSetting;
using ShuklaJi.ModelLayer.Areas.Admin.AppSetting;
using ShuklaJi.ModelLayer.Common.ApiResponse;
using ShuklaJi.Utilities;
using ShuklaJiAPI.Filter;

namespace ShuklaJiAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authentication]
    public class AppSettingController : ControllerBase
    {
        [Route("GetAppSettings")]
        [HttpGet]
        public ApiResponseModel GetAppSettings()
        {
            AppSettingDetails appSettingDetails = new AppSettingDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = appSettingDetails.GetAppSetting();
            return response;
        }

        [Route("GetAppDowntime")]
        [HttpGet]
        public ApiResponseModel GetAppDowntime()
        {
            DowntimeDetails downtimeDetails = new DowntimeDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = downtimeDetails.GetAppDowntime();
            return response;
        }

        [Route("SetAppSetting")]
        [HttpPost]
        public ApiResponseModel SetAppSetting([FromForm] AppSettingModel appSettingData)
        {
            AppSettingDetails appSettingDetails = new AppSettingDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (appSettingData!=null)
            {
                response.Message = appSettingDetails.SetAppSetting(appSettingData); 
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.Error;
            }
            return response;
        }

        [Route("SaveAppDowntime")]
        [HttpPost]
        public ApiResponseModel SaveAppDowntime([FromBody] DowntimeModel model)
        {
            DowntimeDetails downtimeDetails = new DowntimeDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (model != null)
            {
                response.Message = downtimeDetails.SaveAppDowntime(model);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.Error;
            }
            return response;
        }

        [Route("UpdateAppDowntime")]
        [HttpPost]
        public ApiResponseModel UpdateAppDowntime([FromBody] DowntimeModel model)
        {
            DowntimeDetails downtimeDetails = new DowntimeDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (model != null)
            {
                response.Message = downtimeDetails.UpdateAppDowntime(model);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.Error;
            }
            return response;
        }

        [Route("DeleteAppDowntime")]
        [HttpPost]
        public ApiResponseModel DeleteAppDowntime([FromBody] DowntimeModel model)
        {
            DowntimeDetails downtimeDetails = new DowntimeDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (model != null)
            {
                response.Message = downtimeDetails.DeleteAppDowntime(model);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.Error;
            }
            return response;
        }
    }
}