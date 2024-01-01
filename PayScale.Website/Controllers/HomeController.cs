using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewBag.PostalCodes = new SelectList(postalCodes, "PostalCodeId", "PostalCode.Code");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
