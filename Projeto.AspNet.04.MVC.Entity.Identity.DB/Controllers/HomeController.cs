using Microsoft.AspNetCore.Mvc;
using Projeto.AspNet._04.MVC.Entity.Identity.DB.Models;
using System.Diagnostics;

namespace Projeto.AspNet._04.MVC.Entity.Identity.DB.Controllers
{
    // este controller será responsavel pro exercer controle e influencia sobre a área restrita da aplicação
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = 
                Activity.Current?.Id ?? 
                HttpContext.TraceIdentifier });
        }
    }
}