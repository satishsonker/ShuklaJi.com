using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shuklaji.com.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class RoleManagementController : Controller
    {
        [Route("AddRole")]
        public IActionResult AddRoles()
        {
            return View();
        }

        [Route("RoleList")]
        public IActionResult RoleList()
        {
            return View();
        }
    }
}