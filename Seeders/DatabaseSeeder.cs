using MentalHealthAPI.Models;

namespace MentalHealthAPI.Data
{
    public class DatabaseSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Check if the database already has data
            if (!context.Therapists.Any())
            {
                var therapists = new List<Therapist>
                {
                    new Therapist
                    {
                        Name = "Dr. Tyler Petrov",
                        Specialization = "Clinical Psychologist",
                        District = "Yunusobod",
                        City = "Tashkent",
                        About = "A seasoned clinical psychologist with over a decade of experience in helping individuals overcome psychological challenges.",
                        Experience = "10 years in cognitive-behavioral therapy (CBT) and mindfulness techniques.",
                        Image = "path-to-tyler-image.jpg",
                        Price = 20.0m,
                        Appointments = new List<Appointment>()
                    },
                    new Therapist
                    {
                        Name = "Dr. Sarah Lane",
                        Specialization = "Couples Counselor",
                        District = "Mirzo Ulugbek",
                        City = "Tashkent",
                        About = "Specializes in helping couples improve communication and strengthen their relationships.",
                        Experience = "8 years of practice with hundreds of couples.",
                        Image = "path-to-sarah-image.jpg",
                        Price = 50.0m,
                        Appointments = new List<Appointment>()
                    },
                    new Therapist
                    {
                        Name = "Dr. Alex Novak",
                        Specialization = "Child Psychologist",
                        District = "Chilonzor",
                        City = "Tashkent",
                        About = "Expert in child behavioral issues, ADHD, and anxiety disorders.",
                        Experience = "12 years working with children and families.",
                        Image = "path-to-alex-image.jpg",
                        Price = 30.0m,
                        Appointments = new List<Appointment>()
                    }
                    // Add more therapists if needed
                };

                // Add therapists to the context
                context.Therapists.AddRange(therapists);
                context.SaveChanges();
            }
            if (!context.MoodLogs.Any())
            {
                var moodLogs = new List<MoodLog>
            {
                new MoodLog { UserId = 1, Mood = "Happy, Optimistic", Date = DateTime.Now.AddDays(-1) },
                new MoodLog { UserId = 1, Mood = "Sad, Stressed", Date = DateTime.Now.AddDays(-2) },
                new MoodLog { UserId = 1, Mood = "Calm, Hopeful", Date = DateTime.Now.AddDays(-3) },
                new MoodLog { UserId = 1, Mood = "Anxious, Lonely", Date = DateTime.Now.AddDays(-4) },
                new MoodLog { UserId = 1, Mood = "Energetic, Motivated", Date = DateTime.Now.AddDays(-5) },
                new MoodLog { UserId = 1, Mood = "Tired, Disappointed", Date = DateTime.Now.AddDays(-6) },
                new MoodLog { UserId = 1, Mood = "Excited, Proud", Date = DateTime.Now.AddDays(-7) },
                new MoodLog { UserId = 1, Mood = "Grateful, Peaceful", Date = DateTime.Now.AddDays(-8) },
            };

                context.MoodLogs.AddRange(moodLogs);
                context.SaveChanges();
            }

            if (!context.Therapists.Any())
            {
                context.Therapists.AddRange(
                    new Therapist { Name = "Dr. Tyler Petrov", Specialization = "Clinical Psychologist", City = "Tashkent", District = "Yunusobod", About = "Experienced in CBT and mindfulness techniques.", Price = 20, Image = "path/to/image1.jpg" },
                    new Therapist { Name = "Dr. Sarah Smith", Specialization = "Couples Counselor", City = "Tashkent", District = "Chilanzar", About = "Specialized in relationship management.", Price = 50, Image = "path/to/image2.jpg" }
                );
                context.SaveChanges();
            }
            context.SaveChanges();
        }
    }

}

