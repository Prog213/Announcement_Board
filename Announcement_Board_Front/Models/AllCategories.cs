namespace Announcement_Board_Front.Models
{
    public class AllCategories
    {
        public static Dictionary<string, List<string>> AllCategoriesWithSubCategories { get; } = new Dictionary<string, List<string>>
        {
            { "Побутова техніка", new List<string> { "Холодильники", "Пральні машини", "Бойлери", "Печі", "Витяжки", "Мікрохвильові печі" }},
            { "Комп'ютерна техніка", new List<string> { "ПК", "Ноутбуки", "Монітори", "Принтери", "Сканери" }},
            { "Смартфони", new List<string> { "Android смартфони", "iOS/Apple смартфони" }},
            { "Інше", new List<string> { "Одяг", "Взуття", "Аксесуари", "Спортивне обладнання", "Іграшки" }}
        };
    }
}
