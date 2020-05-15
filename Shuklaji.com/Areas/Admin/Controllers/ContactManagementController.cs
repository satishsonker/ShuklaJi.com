using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shuklaji.com.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class ContactManagementController : Controller
    {
        [Route("SaveContactUs")]
        public IActionResult SaveContactUs()
        {
            return View();
        }

        [Route("UserQuery")]
        public IActionResult UserQuery()
        {
            return View();
        }

        [Route("UserFeedback")]
        public IActionResult UserFeedback()
        {
            return View();
        }
    }
}