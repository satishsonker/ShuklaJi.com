using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.ModelLayer.Common.ApiResponse;
using ShuklaJi.ModelLayer.Areas.Admin.RoleManagement;
using ShuklaJi.BusinessLayer.Areas.Admin.RoleManagement;
using ShuklaJi.Utilities;
using ShuklaJiAPI.Filter;

namespace ShuklaJiAPI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authentication]
    public class RoleManagementController : ControllerBase
    {
        [Route("SaveRoles")]
        [HttpPost]
        public ApiResponseModel SaveRoles([FromBody]AddRoleModel addRoleModel)
        {
            RoleDetails roleMaster = new RoleDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = roleMaster.AddRoles(addRoleModel);
            return response;
        }

        [Route("GetRoles")]
        [HttpPost]
        public ApiResponseModel GetRoles([FromQuery] int roleId=0)
        {
            RoleDetails roleMaster = new RoleDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = roleMaster.GetRoles(roleId);
            return response;
        }

        [Route("UpdateRoles")]
        [HttpPost]
        public ApiResponseModel UpdateRoles([FromBody]AddRoleModel updateRoleModel)
        {
            RoleDetails roleMaster = new RoleDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = roleMaster.UpdateRoles(updateRoleModel);
            return response;
        }

        [Route("DeleteRoles")]
        [HttpPost]
        public ApiResponseModel DeleteRoles([FromQuery]int roleId,[FromQuery] int userId)
        {
            RoleDetails roleMaster = new RoleDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Message = roleMaster.DeleteRoles(roleId,userId);
            return response;
        }
    }
}