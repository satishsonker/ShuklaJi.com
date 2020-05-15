using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shuklaji.com.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class MiscellaneousController : Controller
    {
        [Route("RefLookupList")]
        public IActionResult RefLookupList()
        {
            return View();
        }
    }
}