using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221PE_FA22_TrialTest_NgoQuangMinh.Data;

namespace PRN221PE_FA22_TrialTest_NgoQuangMinh.Pages.Candidate
{
    public class CandidateProfileModel : PageModel
    {
        private readonly CandidateManagementContext _candidateManagementContext;

        public CandidateProfileModel(CandidateManagementContext candidateManagementContext)
        {
            _candidateManagementContext = candidateManagementContext;
        }

        [BindProperty]
        public Data.CandidateProfile CandidateProfile { get; set; }

        public List<Data.CandidateProfile> Candidates { get; set; } // List of candidates
        public List<Data.JobPosting> JobPostings { get; set; } // List of job postings

        // OnGet method to fetch candidates and job postings
        public async Task OnGetAsync()
        {
            Candidates = await GetCandidatesAsync(); // Fetch candidates
            JobPostings = await GetJobPostingsAsync(); // Fetch job postings
        }

        // Fetch all candidate profiles
        public async Task<List<Data.CandidateProfile>> GetCandidatesAsync()
        {
            return await _candidateManagementContext.CandidateProfiles
                .Include(u => u.Posting) // Assuming Posting is a navigation property
                .ToListAsync();
        }

        // Fetch all job postings
        public async Task<List<Data.JobPosting>> GetJobPostingsAsync()
        {
            return await _candidateManagementContext.JobPostings.ToListAsync();
        }

        // Handler to get candidate details for JavaScript
        public async Task<IActionResult> OnGetCandidateDetailsAsync(string id)
        {
            var candidate = await _candidateManagementContext.CandidateProfiles
                .Include(c => c.Posting)
                .Where(c => c.CandidateId == id)
                .Select(c => new
                {
                    candidateId = c.CandidateId,
                    fullname = c.Fullname,
                    profileShortDescription = c.ProfileShortDescription,
                    birthday = c.Birthday,
                    profileUrl = c.ProfileUrl,
                    postingId = c.PostingId
                })
                .FirstOrDefaultAsync();

            if (candidate == null)
            {
                return new JsonResult(null); // Return null if not found
            }

            return new JsonResult(candidate);
        }

        public async Task<IActionResult> OnPostCreateCandidateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Add the new candidate to the database
            _candidateManagementContext.CandidateProfiles.Add(CandidateProfile);
            await _candidateManagementContext.SaveChangesAsync();

            // Redirect or return a JSON response
            return new JsonResult(new { success = true, message = "Candidate created successfully" });
        }

    }
}
