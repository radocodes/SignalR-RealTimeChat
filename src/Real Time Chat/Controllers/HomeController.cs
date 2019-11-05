using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Real_Time_Chat.Models;

namespace Real_Time_Chat.Controllers
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

            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NotLogged", "Home");
            }
            else
            {
                return View();
            }
            
        }

        public IActionResult NotLogged()
        {
            return View();
        }

        [Authorize]
        public IActionResult ChatRoom(string roomName)
        {
            ViewData["RoomName"] = roomName;

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
