using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.BusinessLayer.Areas.Admin.ZodiacManagement;
using ShuklaJi.ModelLayer.Areas.Admin.ZodiacManagement;
using ShuklaJi.ModelLayer.Common.ApiResponse;
using ShuklaJi.Utilities;
using ShuklaJiAPI.Filter;

namespace ShuklaJiAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authentication]
    public class ZodiacManagementController : ControllerBase
    {
        [Route("GetZodiacList")]
        [HttpGet]
        public ApiResponseModel GetZodiacList()
        {
            ZodiacDetails zodiacDetails = new ZodiacDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = zodiacDetails.GetZodiacList();
            return response;
        }

        [Route("SaveZodiac")]
        [HttpPost]
        public ApiResponseModel SaveZodiac(ZodiacModel zodiacModel)
        {
            ZodiacDetails zodiacDetails = new ZodiacDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = zodiacDetails.SaveZodiac(zodiacModel);
            return response;
        }

        [Route("SaveZodiacReport")]
        [HttpPost]
        public ApiResponseModel SaveZodiacReport(ZodiacReportModel zodiacReportModel)
        {
            ZodiacDetails zodiacDetails = new ZodiacDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = zodiacDetails.SaveZodiacReport(zodiacReportModel);
            return response;
        }

        [Route("UpdateZodiacReport")]
        [HttpPost]
        public ApiResponseModel UpdateZodiacReport(ZodiacReportModel zodiacReportModel)
        {
            ZodiacDetails zodiacDetails = new ZodiacDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = zodiacDetails.UpdateZodiacReport(zodiacReportModel);
            return response;
        }

        [Route("GetZodiacReport")]
        [HttpGet]
        public ApiResponseModel GetodiacReport(int zodiacReportId=0)
        {
            ZodiacDetails zodiacDetails = new ZodiacDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = zodiacDetails.GetZodiacReport(zodiacReportId);
            return response;
        }
    }
}