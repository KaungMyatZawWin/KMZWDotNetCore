using KMZWDotNetCore.MVCApp2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KMZWDotNetCore.MVCApp2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Hello from View Bag.";
            HomeResponseModel model = new HomeResponseModel()
            {
                AlertMessage = "Data transfer with custom model"
            };
            return View(model);
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
