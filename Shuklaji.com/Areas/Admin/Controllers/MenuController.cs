using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shuklaji.com.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class MenuManagementController : Controller
    {
        [Route("GetMenu")]
        public IActionResult GetMenu()
        {
            return View();
        }

        [Route("MenuList")]
        public IActionResult MenuList()
        {
            return View();
        }
    }
        
}