using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.ModelLayer.Common.ApiResponse;
using ShuklaJi.Utilities;

namespace ShuklaJiAPI.Areas.Web.Controllers
{
    [Area("Web")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ZodiacController : ControllerBase
    {
        [Route("GetHoroscope")]
        [HttpGet]
        public ApiResponseModel GetHoroscope(string reportType,DateTime date,int id)
        {
            ZodiacWebDetails zodiacWebDetails = new ZodiacWebDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = zodiacWebDetails.GetHoroscope(reportType, date,id);
            return response;
        }
    }
}