using System;
using System.ComponentModel.DataAnnotations;

namespace Announcement_Board_Front.Models.ViewModels;

public class AddAnnouncement
{
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public string Status { get; set; } = null!;
    [Required]
    public string Category { get; set; } = null!;
    [Required]
    public string SubCategory { get; set; } = null!;
}
