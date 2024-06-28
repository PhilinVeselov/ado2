using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }
        public IActionResult Greet(string id)
        {
            ViewData["Names"] = id;
            return View();
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(string message)
        {
            ViewData["Message"] = message;
            return View("Edit", message);
        }
    }
}
