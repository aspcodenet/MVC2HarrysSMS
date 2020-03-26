using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarrysSMSWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace HarrysSMSWeb.Controllers
{
    public class ParameterController : Controller
    {
        private readonly IInterestService interestService;

        public ParameterController(IInterestService interestService)
        {
            this.interestService = interestService;
        }
        public IActionResult Index()
        {
            var model = new ViewModels.ParametersViewModel
            {
                CurrentRiksbankenStibor = interestService.GetBaseRate()
            };
            return View(model);
        }
    }
}