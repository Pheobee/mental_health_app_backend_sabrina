namespace MentalHealthAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public int TherapistId { get; set; }
        public string Status { get; set; } // Pending, Confirmed, Canceled
    }
}
