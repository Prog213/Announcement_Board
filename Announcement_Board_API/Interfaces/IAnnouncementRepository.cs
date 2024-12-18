using Announcement_Board_API.DTOs;
using Announcement_Board_API.Models;

namespace Announcement_Board_API.Interfaces;

public interface IAnnouncementRepository
{
    public Task<IEnumerable<Announcement>> GetAnnouncements();
    public Task<Announcement?> GetAnnouncement(int announcementId);
    public Task<bool> AddAnnouncement(CreateAnnouncementDto announcement);
    public Task<bool> UpdateAnnouncement(UpdateAnnouncementDto announcement, int announcementId);
    public Task<bool> DeleteAnnouncement(int announcementId);
}