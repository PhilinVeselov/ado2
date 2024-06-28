using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kt2.Pages
{
    public class IndexModel : PageModel
    {
        private const string FilePath = "text.txt";

        [BindProperty]

        public string Text { get; set; }

        public void OnGet()
        {
            if (System.IO.File.Exists(FilePath))
            {
                Text = System.IO.File.ReadAllText(FilePath);
            }
        }
        public IActionResult OnPost()
        {
            System.IO.File.WriteAllText(FilePath, Text);
            return RedirectToPage("/Index");
        }
    }
}
