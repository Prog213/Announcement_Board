using System;

namespace Announcement_Board_Front.Models.ViewModels;

public class AddAnnouncement
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string SubCategory { get; set; } = null!;
}
