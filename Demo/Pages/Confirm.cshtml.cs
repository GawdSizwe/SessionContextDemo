using Microsoft.AspNetCore.Mvc.RazorPages;
using Demo.Models;
using Demo.Sessions;

namespace Demo.Pages
{
    public class ConfirmModel : PageModel
    {
        public UserData Data { get; set; } = new UserData();
        public void OnGet()
        {
            Data = SessionVar.UserDataValues;
        }
    }
}
