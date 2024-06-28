using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace kt3.Controllers
{
    public class TestController : Controller
    {

        public async Task Text()
        {
            HttpContext.Response.ContentType = "text/plain";
            await HttpContext.Response.WriteAsync("Hello, world!");
        }
        public async Task Html()
        {
            HttpContext.Response.ContentType = "text/html";
            await HttpContext.Response.WriteAsync("<h1>Hello</h1><p>world!</p>");
        }

        public async Task Json()
        {
            HttpContext.Response.ContentType = "application/json";
            var data = new { Name = "Максим", Age = 20 };
            await HttpContext.Response.WriteAsJsonAsync(data);
        }

        public async Task File()
        {
            var filePath = "/Users/phulaveselov/Desktop/kt1/kt3/test.txt";
            HttpContext.Response.ContentType = "text/plain";
            await HttpContext.Response.SendFileAsync(filePath);
        }

        public async Task Status()
        {
            HttpContext.Response.StatusCode = 404;
            await HttpContext.Response.WriteAsync("Not Found");
        }


        public async Task Cookie()
        {
            HttpContext.Response.Cookies.Append("user", "Answer");
            await HttpContext.Response.WriteAsync("Cookie set successfully.");
        }


    }
}
