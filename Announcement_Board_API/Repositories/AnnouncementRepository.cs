using System.Data;
using Announcement_Board_API.DTOs;
using Announcement_Board_API.Interfaces;
using Announcement_Board_API.Models;
using Announcement_Board_API.Params;
using Dapper;

namespace Announcement_Board_API.Repositories;

public class AnnouncementRepository(IDbConnection context) : IAnnouncementRepository
{
    public async Task<int> AddAnnouncement(AnnouncementDto dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Title", dto.Title);
        parameters.Add("Description", dto.Description);
        parameters.Add("Status", dto.Status);
        parameters.Add("Category", dto.Category);
        parameters.Add("SubCategory", dto.SubCategory);

        var createdAnnouncementId = await context.QuerySingleAsync<int>(
            "InsertAnnouncement",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return createdAnnouncementId;
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

    public async Task<IEnumerable<Announcement>> GetAnnouncements(FilterParams filterParams)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Category", filterParams.Category);
        parameters.Add("SubCategory", filterParams.SubCategory);
        return await context.QueryAsync<Announcement>
            ("GetAllAnnouncements", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAnnouncement(AnnouncementDto announcement, int announcementId)
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
