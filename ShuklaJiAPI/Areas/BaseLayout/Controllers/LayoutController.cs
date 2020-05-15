using Microsoft.AspNetCore.Mvc;
using ShuklaJi.BusinessLayer.Areas.BaseLayout.Layout;
using ShuklaJi.ModelLayer.Areas.BaseLayout.Layout;
using ShuklaJi.ModelLayer.Common.ApiResponse;
using ShuklaJi.Utilities;
using ShuklaJiAPI.Filter;
using System.Collections.Generic;
using System.Linq;

namespace ShuklaJiAPI.Areas.BaseLayout.Controllers
{
    [Area("BaseLayout")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authentication]
    public class LayoutController : ControllerBase
    {
        [Route("GetLayoutMenu")]
        [HttpGet]
        public ApiResponseModel GetLayoutMenu()
        {
            LayoutDetails layoutDetails = new LayoutDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok                
            };

            response.Data= layoutDetails.GetLayoutMenu();
            return response;
        }
    }
}
