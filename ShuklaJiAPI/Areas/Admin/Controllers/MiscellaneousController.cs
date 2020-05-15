using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.BusinessLayer.Areas.Admin;
using ShuklaJi.ModelLayer.Areas.Admin.Misc;
using ShuklaJi.ModelLayer.Common.ApiResponse;
using ShuklaJi.Utilities;

namespace ShuklaJiAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class MiscellaneousController : ControllerBase
    {
        [Route("GetReferenceLookup")]
        [HttpGet]
        public ApiResponseModel GetReferenceLookup(string key)
        {
            Miscellaneous miscellaneous = new Miscellaneous();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = miscellaneous.GetReferenceLookup(key);
            return response;
        }

        [Route("SaveReferenceLookup")]
        [HttpPost]
        public ApiResponseModel SaveReferenceLookup(RefLookupModel refLookupModel)
        {
            Miscellaneous miscellaneous = new Miscellaneous();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = miscellaneous.SaveReferenceLookup(refLookupModel);
            return response;
        }

        [Route("UpdateReferenceLookup")]
        [HttpPost]
        public ApiResponseModel UpdateReferenceLookup(RefLookupModel refLookupModel)
        {
            Miscellaneous miscellaneous = new Miscellaneous();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = miscellaneous.UpdateReferenceLookup(refLookupModel);
            return response;
        }

        [Route("DeleteReferenceLookup")]
        [HttpPost]
        public ApiResponseModel DeleteReferenceLookup(RefLookupModel refLookupModel)
        {
            Miscellaneous miscellaneous = new Miscellaneous();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = miscellaneous.DeleteReferenceLookup(refLookupModel);
            return response;
        }
    }
}