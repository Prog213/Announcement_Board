using Announcement_Board_Front.Models;
using Announcement_Board_Front.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Announcement_Board_Front.Pages.Announcements
{
    public class EditAnnouncementModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        [BindProperty]
        public Announcement Announcement { get; set; } = new Announcement();

        public Dictionary<string, List<string>> AllCategoriesWithSubCategories = AllCategories.AllCategoriesWithSubCategories;

        public async Task OnGetAsync(int id)
        {
            var client = httpClientFactory.CreateClient("AnnouncementsClient");

            Announcement = await client.GetFromJsonAsync<Announcement>($"Announcements/{id}")
                            ?? new Announcement();
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
                var notification = new Notification
                {
                    Message = "Announcement successfully updated!",
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
