using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace kt4.Controllers
{
    [Route("[controller]")]
    public class EchoController : Controller
    {
        [HttpGet("Get")]
        public async Task Get()
        {
            Response.ContentType = "text/plain";
            await Response.WriteAsync("GET request received");
        }

        [HttpPost("Post")]
        public async Task Post()
        {
            Response.ContentType = "text/plain";
            await Response.WriteAsync("POST request received");
        }

        [HttpGet("Headers")]
        public async Task Headers()
        {
            Response.ContentType = "application/json";
            var headers = Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());
            await Response.WriteAsJsonAsync(headers);
        }

        [HttpGet("Query")]
        public async Task Query()
        {
            Response.ContentType = "application/json";
            var queryParams = Request.Query.ToDictionary(q => q.Key, q => q.Value.ToString());
            await Response.WriteAsJsonAsync(queryParams);
        }

        [HttpPost("Body")]
        public async Task Body()
        {
            Response.ContentType = "text/plain";
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                await Response.WriteAsync(body);
            }
        }
    }
}
