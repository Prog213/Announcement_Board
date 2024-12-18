using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Announcement_Board_Front.Models;
using System.Net.Http;

namespace Announcement_Board_Front.Pages.Announcements
{
    public class DisplayAnnouncementsModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        public List<Announcement> Announcements { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = httpClientFactory.CreateClient("AnnouncementsClient");

            Announcements = await client.GetFromJsonAsync<List<Announcement>>("Announcements")
                            ?? [];
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var client = httpClientFactory.CreateClient("AnnouncementsClient");
            var response = await client.DeleteAsync($"Announcements/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Announcement successfully deleted!";
                return RedirectToPage();
            }

            TempData["Error"] = "Failed to delete announcement. Try again later.";
            return RedirectToPage();
        }
    }
}
