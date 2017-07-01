using Microsoft.AspNetCore.Mvc;

namespace buildadrink.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
