using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shuklaji.com.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class ZodiacManagementController : Controller
    {
        [Route("ZodiacList")]
        public IActionResult GetZodiacList()
        {
            return View();
        }

        [Route("ZodiacReport")]
        public IActionResult GetZodiacReports()
        {
            return View();
        }

        [Route("SaveZodiacReport")]
        public IActionResult SaveZodiacReports()
        {
            return View();
        }
    }
}