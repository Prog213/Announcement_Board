using Announcement_Board_Front.Models;
using Announcement_Board_Front.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Announcement_Board_Front.Pages.Announcements
{
    public class AddAnnouncementModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        [BindProperty]
        public AddAnnouncement NewAnnouncement { get; set; } = new AddAnnouncement();

        public Dictionary<string, List<string>> AllCategoriesWithSubCategories = AllCategories.AllCategoriesWithSubCategories;

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
                var notification = new Notification
                {
                    Message = "Announcement successfully created!",
                    Type = NotificationType.Success
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/Announcements/Display");
            }

            ViewData["Notification"] = new Notification
            {
                Type = NotificationType.Error,
                Message = "Something went wrong!"
            };

            return Page();

        }
    }
}
