using MentalHealthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentalHealthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TherapistsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TherapistsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Therapists - Get all therapists with optional filters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Therapist>>> GetTherapists(
            [FromQuery] string city = "",
            [FromQuery] string district = "",
            [FromQuery] string specialty = "")
        {
            var query = _context.Therapists.AsQueryable();

            if (!string.IsNullOrEmpty(city))
                query = query.Where(t => t.City == city);

            if (!string.IsNullOrEmpty(district))
                query = query.Where(t => t.District == district);

            if (!string.IsNullOrEmpty(specialty))
                query = query.Where(t => t.Specialization.Contains(specialty));

            return await query.ToListAsync();
        }

        // GET: api/Therapists/{id} - Get a specific therapist by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Therapist>> GetTherapist(int id)
        {
            var therapist = await _context.Therapists.FindAsync(id);
            if (therapist == null)
                return NotFound(new { Message = "Therapist not found." });

            return therapist;
        }

        // POST: api/Therapists - Add a new therapist
        [HttpPost]
        public async Task<ActionResult> AddTherapist([FromBody] Therapist therapist)
        {
            if (therapist == null)
                return BadRequest(new { Message = "Invalid therapist data." });

            _context.Therapists.Add(therapist);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTherapist), new { id = therapist.Id }, therapist);
        }

        // PUT: api/Therapists/{id} - Update an existing therapist
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTherapist(int id, [FromBody] Therapist updatedTherapist)
        {
            if (id != updatedTherapist.Id)
                return BadRequest(new { Message = "Therapist ID mismatch." });

            var therapist = await _context.Therapists.FindAsync(id);
            if (therapist == null)
                return NotFound(new { Message = "Therapist not found." });

            // Update therapist properties
            therapist.Name = updatedTherapist.Name;
            therapist.Specialization = updatedTherapist.Specialization;
            therapist.District = updatedTherapist.District;
            therapist.City = updatedTherapist.City;
            therapist.About = updatedTherapist.About;
            therapist.Experience = updatedTherapist.Experience;
            therapist.Image = updatedTherapist.Image;
            therapist.Price = updatedTherapist.Price;

            _context.Therapists.Update(therapist);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Therapist updated successfully." });
        }

        // DELETE: api/Therapists/{id} - Delete a therapist by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTherapist(int id)
        {
            var therapist = await _context.Therapists.FindAsync(id);
            if (therapist == null)
                return NotFound(new { Message = "Therapist not found." });

            _context.Therapists.Remove(therapist);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Therapist deleted successfully." });
        }
    }
}
