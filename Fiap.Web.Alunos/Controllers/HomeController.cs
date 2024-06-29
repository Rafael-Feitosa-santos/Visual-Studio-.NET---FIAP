using Fiap.Web.Alunos.Logging;
using Fiap.Web.Alunos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fiap.Web.Alunos.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;


        /*public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        private readonly ICustomLogger _customLogger;
        
        public HomeController(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }

        public IActionResult Index()
        {
            _customLogger.Log("Fiap");
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
