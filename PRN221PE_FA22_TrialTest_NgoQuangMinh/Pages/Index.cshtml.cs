using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221PE_FA22_TrialTest_NgoQuangMinh.Pages
{
    public class IndexModel : PageModel
    {
        public string UserEmail { get; private set; } // Property to hold the user email

        public void OnGet()
        {
            // Retrieve the user email from the session
            UserEmail = HttpContext.Session.GetString("UserEmail");
        }
    }
}
