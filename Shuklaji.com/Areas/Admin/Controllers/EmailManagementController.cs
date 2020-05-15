using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shuklaji.com.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class EmailManagementController : Controller
    {
        [Route("GetConfiguration")]
        public IActionResult GetConfiguration()
        {
            return View();
        }

        [Route("GetEmailTemplate")]
        public IActionResult GetEmailTemplate()
        {
            return View();
        }

        [Route("ConfigurationDetails")]
        public IActionResult ConfigurationDetails()
        {
            return View();
        }

        [Route("EmailGroup")]
        public IActionResult EmailGroup()
        {
            return View();
        }
    }
}