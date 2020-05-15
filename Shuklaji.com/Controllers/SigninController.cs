using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shuklaji.com.Models;
using ShuklaJi.ModelLayer.Areas.Admin.UserManagement;
using ShuklaJi.BusinessLayer.Areas.Admin.UserManagement;

namespace Shuklaji.com.Controllers
{
    [Route("[controller]")]
    public class SigninController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        #region Ctor
        
        public SigninController(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        #endregion
        [Route("Login")]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(loginViewModel);
        }
        [Area("Common")]
        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login1(string returnUrl)
        {
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View("~/Views/signin/Login.cshtml", loginViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ExternalLogin")]
        public IActionResult ExternalLogin(string provider,string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Signin",new {ReturnUrl = returnUrl });
            var prop = signInManager.ConfigureExternalAuthenticationProperties(provider,redirectUrl);
            return new ChallengeResult(provider, prop);
        }

        [AllowAnonymous]
        [Route("ExternalLoginCallback")]
        public  async Task<IActionResult> ExternalLoginCallback(string returnUrl,string remoteError)
        {
            returnUrl = returnUrl ?? Url.Content("~/web/home/index");

            LoginViewModel loginViewModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if(remoteError!=null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provide {remoteError}");
                return View("login", loginViewModel);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, $"Error loading external login information");
                return View("login", loginViewModel);
            }
            ExternalUserModel externalUserModel = new ExternalUserModel() { };
            externalUserModel.Email=  info.Principal.FindFirstValue(ClaimTypes.Email);
            externalUserModel.FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
            externalUserModel.LastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
            externalUserModel.Dob = info.Principal.FindFirstValue(ClaimTypes.DateOfBirth)==null?DateTime.Parse("1/1/1753"):DateTime.Parse(info.Principal.FindFirstValue(ClaimTypes.DateOfBirth));
            externalUserModel.Gender = info.Principal.FindFirstValue(ClaimTypes.Gender);
            externalUserModel.Provider = info.LoginProvider;
            externalUserModel.ProviderKey = info.ProviderKey;
            externalUserModel.Mobile = info.Principal.FindFirstValue(ClaimTypes.MobilePhone);
            UserDetails userDetails = new UserDetails(string.Empty);
            var isSaved=userDetails.AddExternalUser(externalUserModel);            
           
            if(!string.IsNullOrEmpty(isSaved.Result.Token))
            {

               return LocalRedirect(returnUrl+$"?txn={isSaved.Result.Token}");
            }

            return View("login", loginViewModel);
        }

        [Route("ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("ForgetPassword")]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [Route("NewPassword")]
        public IActionResult NewPassword()
        {
            return View();
        }
    }
}