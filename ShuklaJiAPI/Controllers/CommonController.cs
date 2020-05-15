using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.BusinessLayer.Common;
using ShuklaJi.ModelLayer.Common.ApiResponse;
using ShuklaJi.Utilities;
using ShuklaJiAPI.Filter;

namespace ShuklaJiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        [Route("IsDataExists")]
        [HttpGet]
        public ApiResponseModel IsDataExists(string key,string value)
        {
            CommonDetails commonDetails = new CommonDetails();
            ApiResponseModel apiResponseModel = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };
            apiResponseModel.Data = commonDetails.IsDataExists(key, value);
            return apiResponseModel;
        }


        [Authentication]
        [Route("DropdownList")]
        [HttpGet]
        public ApiResponseModel DropdownList(string key)
        {
            CommonDetails commonDetails = new CommonDetails();
            ApiResponseModel apiResponseModel = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };
            apiResponseModel.Data = commonDetails.DropdownList(key);
            return apiResponseModel;
        }
    }
}