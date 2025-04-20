using MentalHealthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentalHealthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // Get appointments for a user
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAppointments(int userId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.PatientId == userId)
                .Include(a => a.TherapistId) // Include therapist details
                .OrderBy(a => a.Date)
                .ToListAsync();

            if (!appointments.Any())
                return NotFound(new { Message = "No appointments found for this user." });

            return Ok(appointments);
        }

    

        // Book a new appointment
        [HttpPost]
        public async Task<IActionResult> BookAppointment([FromBody] Appointment appointment)
        {
            if (appointment == null)
                return BadRequest(new { Message = "Invalid appointment data." });

            try
            {
                // Check for existing appointment
                bool isBooked = await _context.Appointments.AnyAsync(a =>
                    a.TherapistId == appointment.TherapistId &&
                    a.Date == appointment.Date);

                if (isBooked)
                    return BadRequest(new { Message = "This time slot is already booked." });

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Appointment booked successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Server error", Error = ex.Message });
            }
        }

        // Cancel an appointment
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound(new { Message = "Appointment not found." });

            var appointmentDateTime = appointment.Date; // Assuming Date includes both date and time
            var currentTime = DateTime.UtcNow;

            // Calculate the time difference in hours
            var timeDifference = (appointmentDateTime - currentTime).TotalHours;

            if (timeDifference < 2)
            {
                return BadRequest(new
                {
                    Message = "You can't cancel the appointment when it is less than 2 hours left. Payment will not be refunded."
                });
            }

            // Remove the appointment
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Your appointment has been cancelled successfully. Please reschedule for your mental health."
            });
        }
    }
}
