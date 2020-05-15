using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shuklaji.com.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class UserManagementController : Controller
    {
        [Route("AddUser")]
        public IActionResult AddUser()
        {
            return View();
        }

        [Route("UserList")]
        public IActionResult UserList()
        {
            return View();
        }

        [Route("Astrologers")]
        public IActionResult Astrologers()
        {
            return View();
        }

        [Route("AddAstrologers")]
        public IActionResult AddAstrologers()
        {
            return View();
        }

        [Route("ViewAstrologer")]
        public IActionResult ViewAstrologer()
        {
            bool isAdmin = false;
            if(Request.Query.ContainsKey("txntype"))
            {
                isAdmin = Request.Query["txntype"].ToString() == "admin" ? true : false;
            }
            return View(isAdmin);
        }

        [Route("AstrologerSchedule")]
        public IActionResult AstrologerSchedule()
        {
            return View();
        }
    }
}