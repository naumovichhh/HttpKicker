using Http_Kicker.Models;
using Http_Kicker.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Http_Kicker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KickerService _kickerService;

        public HomeController(ILogger<HomeController> logger, KickerService kickerService)
        {
            _logger = logger;
            _kickerService = kickerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Settings()
        {
            Settings settings = _kickerService.ReadSettings();
            return View(settings);
        }

        [HttpPost]
        public IActionResult Save(Settings settings)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Settings), settings);
            }

            _kickerService.ApplySettings(settings);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
