using Microsoft.AspNetCore.Mvc;
using kt6.Data;
using kt6.Models;
using System.Linq;

namespace kt6.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return RedirectToAction("Error");
            }
            return View(product);
        }

        public IActionResult Error()
        {
            ViewBag.ErrorMessage = "Продукт не найден";
            return View();
        }
    }
}
