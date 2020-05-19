using System.Diagnostics;
using System.Threading.Tasks;
using ASP.NET_Core3.x_MVC_Demo.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASP.NET_Core3.x_MVC_Demo.Models;
using Demo.Dto.Dtos;

namespace ASP.NET_Core3.x_MVC_Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoApiHttpClient _demoApiHttpClient;

        public HomeController(ILogger<HomeController> logger, DemoApiHttpClient _demoApiHttpClient)
        {
            _logger = logger;
            this._demoApiHttpClient = _demoApiHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _demoApiHttpClient.GetEmployeesAsync(null);
            return View(employees);
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
