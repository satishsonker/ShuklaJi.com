﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shuklaji.com.Areas.Web.Controllers
{
    [Area("Web")]
    [Route("Web/[controller]")]
    public class ZodiacController : Controller
    {
        [Route("ViewZodiac")]
        public IActionResult ViewZodiac()
        {
            return View();
        }
    }
}