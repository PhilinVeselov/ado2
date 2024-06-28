using Microsoft.AspNetCore.Mvc;

namespace kt6.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("List", "Product");
        }
    }
}
