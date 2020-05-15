using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.BusinessLayer.Areas.Admin.UserManagement;
using ShuklaJi.ModelLayer.Areas.Admin.UserManagement;
using ShuklaJi.ModelLayer.Common.ApiResponse;
using ShuklaJi.Utilities;
using ShuklaJiAPI.Filter;
using System.Collections.Generic;
using System.IO;

namespace ShuklaJiAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authentication]
    public class UserManagementController : ControllerBase
    {
        IHostingEnvironment iHostingEnvironment;
        UserDetails userDetails = null;
        public UserManagementController(IHostingEnvironment hostingEnvironment)
        {
            iHostingEnvironment = hostingEnvironment;
            iHostingEnvironment.WebRootPath = iHostingEnvironment.ContentRootPath;
           
            userDetails = new UserDetails(iHostingEnvironment.WebRootPath);
        }
        [Route("GetUser")]
        [HttpGet]
        public ApiResponseModel GetUser([FromQuery] int userId = 0)
        {
            
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            response.Data = userDetails.GetUser(userId);
            return response;
        }

        [Route("AddUser")]
        [HttpPost]
        public ApiResponseModel AddUser([FromForm] UserModel menuModel)
        {
            
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (menuModel != null)
            {
                response.Message = userDetails.AddUser(menuModel);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
            }
            return response;
        }

        [Route("UpdateUser")]
        [HttpPost]
        public ApiResponseModel UpdateUser([FromForm] UserModel menuModel)
        {
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (menuModel != null)
            {
                response.Message = userDetails.UpdateUser(menuModel);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
            }
            return response;
        }

        [Route("DeleteUser")]
        [HttpPost]
        public ApiResponseModel DeleteUser([FromQuery] int modifiedBy, [FromQuery] int userId)
        {
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (modifiedBy > 0 && userId > 0)
            {
                response.Message = userDetails.DeleteUser(modifiedBy, userId);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
            }
            return response;
        }

        [Route("ActiveDeactiveUser")]
        [HttpPost]
        public ApiResponseModel ActiveDeactiveUser([FromQuery] int modifiedBy, [FromQuery] int userId, [FromQuery] bool status)
        {      
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (modifiedBy > 0 && userId > 0)
            {
                response.Message = userDetails.ActiveDeactiveUser(modifiedBy, userId, status);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
            }
            return response;
        }

        [Route("SaveAstrologers")]
        [HttpPost]
        public ApiResponseModel SaveAstrologers([FromForm] AstrologerModel astrologerModel)
        {
            AstrologerDetails astrologerDetails = new AstrologerDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (astrologerModel!=null)
            {
                response.Message = astrologerDetails.SaveAstrologers(astrologerModel,iHostingEnvironment.WebRootPath);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
                response.StatusCode = UtilityConstant.StatusCode.BadRequest;
                response.Status = UtilityConstant.Status.BadRequest;
            }
            return response;
        }

        [Route("UpdateAstrologers")]
        [HttpPost]
        public ApiResponseModel UpdateAstrologers([FromForm] AstrologerModel astrologerModel)
        {
            AstrologerDetails astrologerDetails = new AstrologerDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (astrologerModel != null)
            {
                response.Message = astrologerDetails.UpdateAstrologers(astrologerModel, iHostingEnvironment.WebRootPath);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
                response.StatusCode = UtilityConstant.StatusCode.BadRequest;
                response.Status = UtilityConstant.Status.BadRequest;
            }
            return response;
        }

        [Route("DeleteAstrologers")]
        [HttpPost]
        public ApiResponseModel DeleteAstrologers([FromBody] AstrologerModel astrologerModel)
        {
            AstrologerDetails astrologerDetails = new AstrologerDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (astrologerModel != null)
            {
                response.Message = astrologerDetails.DeleteAstrologers(astrologerModel);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
                response.StatusCode = UtilityConstant.StatusCode.BadRequest;
                response.Status = UtilityConstant.Status.BadRequest;
            }
            return response;
        }

        [Route("GetAstrologers")]
        [HttpGet]
        public ApiResponseModel GetAstrologers([FromQuery] int astrologerId=0)
        {
            AstrologerDetails astrologerDetails = new AstrologerDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (astrologerId >-1)
            {
                response.Data = astrologerDetails.GetAstrologers(astrologerId);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
                response.StatusCode = UtilityConstant.StatusCode.BadRequest;
                response.Status = UtilityConstant.Status.BadRequest;
            }
            return response;
        }

        [Route("GetAstrologerSchedule")]
        [HttpGet]
        public ApiResponseModel GetAstrologerSchedule([FromQuery] int astrologerId)
        {
            AstrologerDetails astrologerDetails = new AstrologerDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (astrologerId > -1)
            {
                response.Data = astrologerDetails.GetAstrologerSchedule(astrologerId);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
                response.StatusCode = UtilityConstant.StatusCode.BadRequest;
                response.Status = UtilityConstant.Status.BadRequest;
            }
            return response;
        }

        [Route("SaveAstrologerSchedule")]
        [HttpPost]
        public ApiResponseModel SaveAstrologerSchedule([FromBody] List<AstrologerScheduleModel> model)
        {
            AstrologerDetails astrologerDetails = new AstrologerDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (model!=null)
            {
                response.Message = astrologerDetails.SaveAstrologerSchedule(model);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
                response.StatusCode = UtilityConstant.StatusCode.BadRequest;
                response.Status = UtilityConstant.Status.BadRequest;
            }
            return response;
        }

        [Route("UpdateAstrologerSchedule")]
        [HttpPost]
        public ApiResponseModel UpdateAstrologerSchedule([FromBody] AstrologerScheduleModel model)
        {
            AstrologerDetails astrologerDetails = new AstrologerDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (model != null)
            {
                response.Message = astrologerDetails.UpdateAstrologerSchedule(model);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
                response.StatusCode = UtilityConstant.StatusCode.BadRequest;
                response.Status = UtilityConstant.Status.BadRequest;
            }
            return response;
        }

        [Route("DeleteAstrologerSchedule")]
        [HttpPost]
        public ApiResponseModel DeleteAstrologerSchedule([FromBody] AstrologerScheduleModel model)
        {
            AstrologerDetails astrologerDetails = new AstrologerDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (model != null)
            {
                response.Message = astrologerDetails.DeleteAstrologerSchedule(model);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
                response.StatusCode = UtilityConstant.StatusCode.BadRequest;
                response.Status = UtilityConstant.Status.BadRequest;
            }
            return response;
        }


        [Route("GetAstrologerRating")]
        [HttpGet]
        public ApiResponseModel GetAstrologerRating([FromQuery] int astrologerId)
        {
            AstrologerDetails astrologerDetails = new AstrologerDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (astrologerId > -1)
            {
                response.Data = astrologerDetails.GetAstrologerRating(astrologerId);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
                response.StatusCode = UtilityConstant.StatusCode.BadRequest;
                response.Status = UtilityConstant.Status.BadRequest;
            }
            return response;
        }

        [Route("SaveAstrologerRating")]
        [HttpPost]
        public ApiResponseModel SaveAstrologerRating([FromBody] AstrologerRatingModel astrologerRatingModel)
        {
            AstrologerDetails astrologerDetails = new AstrologerDetails();
            ApiResponseModel response = new ApiResponseModel()
            {
                StatusCode = UtilityConstant.StatusCode.Ok,
                Status = UtilityConstant.Status.Ok
            };

            if (astrologerRatingModel!=null)
            {
                response.Message = astrologerDetails.SaveAstrologerRating(astrologerRatingModel);
            }
            else
            {
                response.Message = UtilityConstant.UserResponseStatus.NoDataFromClient;
                response.StatusCode = UtilityConstant.StatusCode.BadRequest;
                response.Status = UtilityConstant.Status.BadRequest;
            }
            return response;
        }
    }
}