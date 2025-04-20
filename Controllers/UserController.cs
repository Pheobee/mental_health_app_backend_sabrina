using MentalHealthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentalHealthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // Get all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // Get users by role
        [HttpGet("role/{role}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByRole(string role)
        {
            return await _context.Users
                .Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        // Get users paged
        [HttpGet("paged")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersPaged(int page = 1, int pageSize = 10)
        {
            return await _context.Users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Get user by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound("User not found.");
            return user;
        }

        // Search users by name or email
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsers(string query)
        {
            return await _context.Users
                .Where(u => u.Name.Contains(query) || u.Email.Contains(query))
                .ToListAsync();
        }

        // Update user details
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User updatedUser)
        {
            if (id != updatedUser.Id) return BadRequest("User ID mismatch.");
            if (!await _context.Users.AnyAsync(u => u.Id == id)) return NotFound("User not found.");

            _context.Entry(updatedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound("User not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
