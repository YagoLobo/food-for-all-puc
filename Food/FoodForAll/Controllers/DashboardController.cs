using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodForAll.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Doador()
        {
            return View();
        }
        public IActionResult Receptor()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }
    }
} 