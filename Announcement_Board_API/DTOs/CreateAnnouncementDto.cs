namespace Announcement_Board_API.DTOs;

public class CreateAnnouncementDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string SubCategory { get; set; } = null!;
}
