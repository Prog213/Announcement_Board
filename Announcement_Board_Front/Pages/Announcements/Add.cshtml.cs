using Announcement_Board_Front.Models;
using Announcement_Board_Front.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Announcement_Board_Front.Pages.Announcements
{
    public class AddAnnouncementModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        [BindProperty]
        public AddAnnouncement NewAnnouncement { get; set; } = new AddAnnouncement();

        public Dictionary<string, List<string>> AllCategoriesWithSubCategories = AllCategories.AllCategoriesWithSubCategories;

        public List<string> AvailableSubCategories { get; set; } = new List<string>();

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = httpClientFactory.CreateClient("AnnouncementsClient");
            var response = await client.PostAsJsonAsync("Announcements", NewAnnouncement);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Announcement successfully created!";
                return RedirectToPage("/Announcements/Display");
            }

            ModelState.AddModelError(string.Empty, "Failed to create announcement. Try again later.");
            return Page();

        }
    }
}
