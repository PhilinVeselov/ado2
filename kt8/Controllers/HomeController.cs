using Microsoft.AspNetCore.Mvc;

namespace kt8.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Encryption");
        }
    }
}
