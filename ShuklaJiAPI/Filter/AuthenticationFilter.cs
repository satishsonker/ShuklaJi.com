
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ShuklaJi.ModelLayer.Common;
using ShuklaJi.Utilities;
using System;
using System.Web;

namespace ShuklaJiAPI.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AuthenticationAttribute : ActionFilterAttribute, IActionFilter, IResultFilter
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //if (!context.HttpContext.Response.Headers.ContainsKey("refreshToken"))
            //{
            //    context.HttpContext.Response.Headers.Add("refreshToken", "Test");

            //}
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers["authorization"].ToString();
            if (string.IsNullOrEmpty(token))
            {
                //context.HttpContext.Response.StatusCode = 401;
                if (!context.HttpContext.Response.Headers.ContainsKey("invalidToken"))
                {
                    context.HttpContext.Response.Headers.Add("invalidToken", "true");
                }
                //context.Result = new JsonResult(new { Message = "InvalidToken" });
            }
            else
            {
                var json = EncryptManager.Decrypt(token, AppSetting.GetAppSettingsData(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.SaltKey, string.Empty));
                var tokenModel = JsonConvert.DeserializeObject<LoginTokenModel>(json); 
                int refreshTokenTime = AppSetting.GetAppSettingsData<int>(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.SaltKey, string.Empty);
                if (tokenModel==null || tokenModel.ExpireTime<DateTime.Now)
                {
                    //context.HttpContext.Response.StatusCode = 401;
                    if (!context.HttpContext.Response.Headers.ContainsKey("expireToken"))
                    {
                        context.HttpContext.Response.Headers.Add("expireToken", "true");
                    }
                    //context.Result = new JsonResult(new {Message= "ExpireToken" });
                }
                else
                {
                    if (tokenModel.ExpireTime > DateTime.Now.AddMinutes(-refreshTokenTime) && tokenModel.ExpireTime > DateTime.Now)
                    {
                        tokenModel.ExpireTime = tokenModel.ExpireTime.AddMinutes(15);
                        LoginTokenDataModel loginTokenDataModel = new LoginTokenDataModel();
                        loginTokenDataModel.Message = "RefreshToken";
                        loginTokenDataModel.Token = EncryptManager.Encrypt(JsonConvert.SerializeObject(tokenModel), AppSetting.GetAppSettingsData(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.SaltKey, string.Empty));
                        loginTokenDataModel.UserData = tokenModel.UserData;
                        //context.Result = new JsonResult(loginTokenDataModel);
                        if (!context.HttpContext.Response.Headers.ContainsKey("refreshToken"))
                        {
                            context.HttpContext.Response.Headers.Add("refreshToken", JsonConvert.SerializeObject(loginTokenDataModel));
                        }
                    }
                }
            }
        }
    }
}
