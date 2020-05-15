using Microsoft.AspNetCore.Mvc;
using ModelLayer.Areas.Admin.MenuManagement;
using ShuklaJi.BusinessLayer.Areas.Admin.MenuManagement;
using ShuklaJi.ModelLayer.Common.ApiResponse;
using ShuklaJi.Utilities;
using ShuklaJiAPI.Filter;

namespace ShuklaJiAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authentication]
    public class MenuManagementController : ControllerBase
    {
        [Route("GetMenu")]
        [HttpGet]
        public ApiResponseModel GetMenu([FromQuery] int menuId=0)
        {
            MenuDetails menuDetails = new MenuDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = menuDetails.GetMenu(menuId);
            return response;
        }

        [Route("AddMenu")]
        [HttpPost]
        public ApiResponseModel AddMenu([FromBody] MenuModel menuModel)
        {
            MenuDetails menuDetails = new MenuDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (menuModel!=null)
            {
                response.Message = menuDetails.AddMenu(menuModel); 
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
            }
            return response;
        }

        [Route("UpdateMenu")]
        [HttpPost]
        public ApiResponseModel UpdateMenu([FromBody] MenuModel menuModel)
        {
            MenuDetails menuDetails = new MenuDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (menuModel != null)
            {
                response.Message = menuDetails.UpdateMenu(menuModel);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
            }
            return response;
        }

        [Route("DeleteMenu")]
        [HttpPost]
        public ApiResponseModel DeleteMenu([FromBody] MenuModel menuModel)
        {
            MenuDetails menuDetails = new MenuDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (menuModel != null)
            {
                response.Message = menuDetails.DeleteMenu(menuModel);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
            }
            return response;
        }
    }
}