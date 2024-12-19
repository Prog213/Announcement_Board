using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Announcement_Board_Front.Models;
using System.Net.Http;
using Announcement_Board_Front.Models.ViewModels;
using System.Text.Json;

namespace Announcement_Board_Front.Pages.Announcements
{
    public class DisplayAnnouncementsModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        public List<Announcement> Announcements { get; set; } = new();

        public async Task OnGetAsync()
        {
            var notificationJson = TempData["Notification"] as string;
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

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
                var notification = new Notification
                {
                    Message = "Announcement successfully deleted!",
                    Type = NotificationType.Success
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage();
            }

            Announcements = await client.GetFromJsonAsync<List<Announcement>>("Announcements") ?? [];

            ViewData["Notification"] = new Notification
            {
                Type = NotificationType.Error,
                Message = "Something went wrong!"
            };
            return Page();
        }
    }
}
