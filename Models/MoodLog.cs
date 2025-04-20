namespace MentalHealthAPI.Models
{
    public class MoodLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Mood { get; set; } 
        public DateTime Date { get; set; }
    }
}
