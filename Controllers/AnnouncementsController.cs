using Announcement_Board_API.DTOs;
using Announcement_Board_API.Interfaces;
using Announcement_Board_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Announcement_Board_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController(IAnnouncementRepository repo) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncements()
        {
            var announcements = await repo.GetAnnouncements();
            return Ok(announcements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetAnnouncement(int id)
        {
            var announcement = await repo.GetAnnouncement(id);
            if (announcement == null)
                return NotFound();
                
            return Ok(announcement);
        }

        [HttpPost]
        public async Task<ActionResult<Announcement>> CreateAnnouncment(CreateAnnouncementDto announcement)
        {
            await repo.AddAnnouncement(announcement);
            return CreatedAtAction("GetAnnouncement", new { id = announcement.Id }, announcement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, UpdateAnnouncementDto announcement)
        {
            if(await repo.UpdateAnnouncement(announcement, id))
                return NoContent();
            
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            if (await repo.DeleteAnnouncement(id))
                return NoContent();
            
            return NotFound();
        }
    }
}
