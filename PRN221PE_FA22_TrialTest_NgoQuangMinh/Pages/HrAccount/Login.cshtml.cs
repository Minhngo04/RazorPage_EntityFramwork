using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221PE_FA22_TrialTest_NgoQuangMinh.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PRN221PE_FA22_TrialTest_NgoQuangMinh.Pages.HrAccount
{
    public class Login : PageModel
    {
        private readonly CandidateManagementContext _candidateManagementContext;

        public Login(CandidateManagementContext candidateManagementContext)
        {
            _candidateManagementContext = candidateManagementContext;
        }

        [BindProperty]
        public Data.Hraccount Hraccount { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Use the GetHraccount method to find the account by email and password
            var existingHraccount = GetHraccount(Hraccount.Email, Hraccount.Password);

            if (existingHraccount == null)
            {
                TempData["error"] = "Account not found or password is incorrect.";
                return Page();
            }

            // Set the user email in session
            HttpContext.Session.SetString("UserEmail", existingHraccount.Email);

            // Only set success message if session is successfully created
            if (HttpContext.Session.GetString("UserEmail") != null)
            {
                TempData["success"] = "Logged in successfully!";
            }

            return RedirectToPage("/Index"); // Redirect to the home page
        }

        // Changed method name to GetHraccount to match its purpose and added password check
        public Hraccount GetHraccount(string email, string password)
        {
            return _candidateManagementContext.Hraccounts
                .SingleOrDefault(n => n.Email.Equals(email) && n.Password.Equals(password)); // Check both email and password
        }
    }
}
