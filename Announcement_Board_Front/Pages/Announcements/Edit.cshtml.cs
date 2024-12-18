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

        public List<string> Categories { get; set; } = new List<string>
        {
            "Побутова техніка", "Комп'ютерна техніка", "Смартфони", "Інше"
        };

        public Dictionary<string, List<string>> SubCategories { get; set; } = new Dictionary<string, List<string>>
        {
            { "Побутова техніка", new List<string> { "Холодильники", "Пральні машини", "Бойлери", "Печі", "Витяжки", "Мікрохвильові печі" }},
            { "Комп'ютерна техніка", new List<string> { "ПК", "Ноутбуки", "Монітори", "Принтери", "Сканери" }},
            { "Смартфони", new List<string> { "Android смартфони", "iOS/Apple смартфони" }},
            { "Інше", new List<string> { "Одяг", "Взуття", "Аксесуари", "Спортивне обладнання", "Іграшки" }}
        };

        public List<string> AvailableSubCategories { get; set; } = new List<string>();

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
                TempData["Success"] = "Announcement successfully updated!";
                return RedirectToPage("/Announcements/Display");
            }

            ModelState.AddModelError(string.Empty, "Failed to update announcement. Try again later.");
            return Page();

        }
    }
}
