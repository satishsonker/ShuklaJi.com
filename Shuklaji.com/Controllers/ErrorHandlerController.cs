using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.BusinessLayer.Areas.Admin.AppSetting;
using ShuklaJi.ModelLayer.Areas.Admin.AppSetting;

namespace Shuklaji.com.Controllers
{
    [Route("common/[controller]")]
    public class ErrorHandlerController : Controller
    {
        [Route("Unauthorize")]
        public IActionResult Unauthorize()
        {
            return View();
        }

        [Route("Downtime")]
        public IActionResult Downtime()
        {
            DowntimeDetails downtimeDetails = new DowntimeDetails();
            var downtime = downtimeDetails.HasAppDowntime();
            return View(downtime);
        }
    }
}