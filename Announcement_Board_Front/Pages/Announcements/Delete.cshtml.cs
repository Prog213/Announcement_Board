using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace Announcement_Board_Front.Pages.Announcements
{
    public class DeleteModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        public async Task<ActionResult> OnGetAsync(int id)
        {
            var client = httpClientFactory.CreateClient("AnnouncementsClient");

            var response = await client.DeleteAsync($"Announcements/{id}");

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
