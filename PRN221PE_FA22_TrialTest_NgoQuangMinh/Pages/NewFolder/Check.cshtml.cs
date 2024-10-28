using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.NewFolder
{
    public class CheckModel : PageModel
    {
        public string Name = "";
        public void OnGet()
        {
            Name = "John Doe";
        }
        public void OnPost()
        {
        }
        public ActionResult OnPostTest(Person per)
        {
            return Content("It works: " + per.FirstName + " " + per.LastName + " " + per.Gender);
        }
    }
  

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Gender { get; set; } // Changed from int to string
}
}
