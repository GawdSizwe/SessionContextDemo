using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Demo.Models;
using Demo.Sessions;
using System.Text.Json;

namespace Demo.Pages
{
    public class MoreDetailModel : PageModel
    {
        [BindProperty] public UserData Input { get; set; } = new UserData();

        public void OnGet()
        {
            Input.FirstName = SessionVar.FirstName;
        }

        public IActionResult OnPost(UserData inputData)
        {
            SessionVar.UserDataValues = inputData;

            return RedirectToPage("confirm");
        }
    }
}