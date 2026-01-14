using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Demo.Sessions;

namespace Demo.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost(string name)
        {
            SessionVar.FirstName = name;
            return RedirectToPage("moredetail");
        }
    }
}