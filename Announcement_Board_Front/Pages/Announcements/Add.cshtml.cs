using Announcement_Board_Front.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Announcement_Board_Front.Pages.Announcements
{
    public class AddAnnouncementModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        [BindProperty]
        public AddAnnouncement NewAnnouncement { get; set; } = new AddAnnouncement();

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
