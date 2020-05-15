using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.BusinessLayer.Areas.Admin.Dashboard;
using ShuklaJi.ModelLayer.Common.ApiResponse;
using ShuklaJi.Utilities;
using ShuklaJiAPI.Filter;

namespace ShuklaJiAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authentication]
    public class DashboardController : ControllerBase
    {
        [Route("GetDashboardCount")]
        [HttpGet]
        public ApiResponseModel GetDashboardCount()
        {
            DashboardDetails dashboardDetails = new DashboardDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = dashboardDetails.GetDashboardCount();
            return response;
        }
    }
}