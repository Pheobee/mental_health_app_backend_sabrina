using MentalHealthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentalHealthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoodLogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoodLogsController(AppDbContext context)
        {
            _context = context;
        }

        // Add a new mood log
        [HttpPost]
        public async Task<IActionResult> AddMoodLog([FromBody] MoodLog moodLog)
        {
            if (moodLog == null)
                return BadRequest(new { Message = "Invalid mood log data." });

            _context.MoodLogs.Add(moodLog);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Mood log added successfully." });
        }

        // Get mood logs for a user
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetMoodLogs(int userId)
        {
            var moodLogs = await _context.MoodLogs
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.Date)
                .ToListAsync();

            if (!moodLogs.Any())
                return NotFound(new { Message = "No mood logs found for this user." });

            return Ok(moodLogs);
        }
    }
}
