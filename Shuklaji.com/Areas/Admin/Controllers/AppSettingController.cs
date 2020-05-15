using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shuklaji.com.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class AppSettingController : Controller
    {
        [Route("GetAppSetting")]
        public IActionResult GetAppSetting()
        {
            return View();
        }

        [Route("AppDowntime")]
        public IActionResult AppDowntime()
        {
            return View();
        }
    }
}