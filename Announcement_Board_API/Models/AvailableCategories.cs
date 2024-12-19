namespace Announcement_Board_API.Models
{
    public static class AvailableCategories
    {
        public static Dictionary<string, List<string>> ValidCategories = new()
        {
            { "Побутова техніка", new List<string> { "Холодильники", "Пральні машини", "Бойлери", "Печі", "Витяжки", "Мікрохвильові печі" }},
            { "Комп'ютерна техніка", new List<string> { "ПК", "Ноутбуки", "Монітори", "Принтери", "Сканери" }},
            { "Смартфони", new List<string> { "Android смартфони", "iOS/Apple смартфони" }},
            { "Інше", new List<string> { "Одяг", "Взуття", "Аксесуари", "Спортивне обладнання", "Іграшки" }}
        };
    }
}
