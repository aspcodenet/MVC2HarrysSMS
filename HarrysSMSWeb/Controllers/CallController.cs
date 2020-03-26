using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HarrysSMSWeb.Controllers
{
    public class CallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}