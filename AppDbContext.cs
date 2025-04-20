using MentalHealthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MentalHealthAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MoodLog> MoodLogs { get; set; }
    }

}
