namespace Announcement_Board_Front.Models.ViewModels
{
    public class Notification
    {
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
    }
}
