using System.Data;
using Announcement_Board_API.DTOs;
using Announcement_Board_API.Interfaces;
using Announcement_Board_API.Models;
using Dapper;

namespace Announcement_Board_API.Repositories;

public class AnnouncementRepository(IDbConnection context) : IAnnouncementRepository
{
    public async Task<bool> AddAnnouncement(CreateAnnouncementDto dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Title", dto.Title);
        parameters.Add("Description", dto.Description);
        parameters.Add("Status", dto.Status);
        parameters.Add("Category", dto.Category);
        parameters.Add("SubCategory", dto.SubCategory);

        var rowsAffected = await context.ExecuteAsync
            ("InsertAnnouncement", parameters, commandType: CommandType.StoredProcedure);
        
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAnnouncement(int announcementId)
    {
        var rowsAffected = await context.ExecuteAsync
            ("DeleteAnnouncement", new { Id = announcementId }, commandType: CommandType.StoredProcedure);
        
        return rowsAffected > 0;
    }

    public async Task<Announcement?> GetAnnouncement(int announcementId)
    {
        return await context.QueryFirstOrDefaultAsync<Announcement>(
            "GetAnnouncementById",
            new { Id = announcementId },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<Announcement>> GetAnnouncements()
    {
        return await context.QueryAsync<Announcement>
            ("GetAllAnnouncements", commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAnnouncement(UpdateAnnouncementDto announcement, int announcementId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", announcementId);
        parameters.Add("Title", announcement.Title);
        parameters.Add("Description", announcement.Description);
        parameters.Add("Status", announcement.Status);
        parameters.Add("Category", announcement.Category);
        parameters.Add("SubCategory", announcement.SubCategory);

        var rowsAffected = await context.ExecuteAsync
            ("UpdateAnnouncement", parameters, commandType: CommandType.StoredProcedure);
        
        return rowsAffected > 0;
    }
}
