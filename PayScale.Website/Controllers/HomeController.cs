using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PayScale.Models;
using PayScale.Website.Clients;
using PayScale.Website.Clients.IClientServices;
using PayScale.Website.Models;
using System.Diagnostics;

namespace PayScale.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClientService _clientService;

        public HomeController(ILogger<HomeController> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            var postalCodes =  await _clientService.GetPostalCodes();
            ViewBag.PostalCodes = new SelectList(postalCodes, "PostalCode.Id", "PostalCode.Code");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(TaxCalculation taxCalculation)
        {
            if(!ModelState.IsValid)
            {
                return View(taxCalculation);

            }

           var res =  await _clientService.SubmitCalculatedTax(taxCalculation);
  
            return PartialView("_Calculation", res);
        }


        public IActionResult Privacy()
        {
            return View();
        }


    }
}
