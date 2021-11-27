using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ofarz_rest_api.Controllers.FundController
{
    public class SoftKarrotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}