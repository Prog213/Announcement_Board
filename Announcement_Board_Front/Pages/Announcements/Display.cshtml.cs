using Announcement_Board_Front.Models;
using Announcement_Board_Front.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Announcement_Board_Front.Pages.Announcements
{
    public class DisplayAnnouncementsModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        public Dictionary<string, List<string>> AllCategoriesWithSubCategories = AllCategories.AllCategoriesWithSubCategories;
        public List<Announcement> Announcements { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Category { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SubCategory { get; set; }

        public async Task OnGetAsync(string? Category, string? SubCategory)
        {

            var notificationJson = TempData["Notification"] as string;
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

            var client = httpClientFactory.CreateClient("AnnouncementsClient");

            Announcements = await client.GetFromJsonAsync<List<Announcement>>
                ($"Announcements?Category={Category}&SubCategory={SubCategory}") ?? [];
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
