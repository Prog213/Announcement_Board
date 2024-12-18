using Announcement_Board_Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace Announcement_Board_Front.Pages.Announcements
{
    public class EditAnnouncementModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        [BindProperty]
        public Announcement Announcement { get; set; } = new Announcement();

        public Dictionary<string, List<string>> AllCategoriesWithSubCategories = AllCategories.AllCategoriesWithSubCategories;

        public List<string> AvailableSubCategories { get; set; } = new List<string>();

        public async Task OnGetAsync(int id)
        {
            var client = httpClientFactory.CreateClient("AnnouncementsClient");

            Announcement = await client.GetFromJsonAsync<Announcement>($"Announcements/{id}")
                            ?? new Announcement();

            if (!string.IsNullOrEmpty(Announcement.Category) && AllCategoriesWithSubCategories.ContainsKey(Announcement.Category))
            {
                AvailableSubCategories = AllCategoriesWithSubCategories[Announcement.Category];
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = httpClientFactory.CreateClient("AnnouncementsClient");
            var response = await client.PutAsJsonAsync($"Announcements/{Announcement.Id}", Announcement);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Announcement successfully updated!";
                return RedirectToPage("/Announcements/Display");
            }

            ModelState.AddModelError(string.Empty, "Failed to update announcement. Try again later.");
            return Page();

        }
    }
}
