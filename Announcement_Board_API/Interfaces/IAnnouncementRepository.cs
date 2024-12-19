using Announcement_Board_API.DTOs;
using Announcement_Board_API.Models;
using Announcement_Board_API.Params;

namespace Announcement_Board_API.Interfaces;

public interface IAnnouncementRepository
{
    public Task<IEnumerable<Announcement>> GetAnnouncements(FilterParams filterParams);
    public Task<Announcement?> GetAnnouncement(int announcementId);
    public Task<int> AddAnnouncement(AnnouncementDto announcement);
    public Task<bool> UpdateAnnouncement(AnnouncementDto announcement, int announcementId);
    public Task<bool> DeleteAnnouncement(int announcementId);
}