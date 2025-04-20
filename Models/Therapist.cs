namespace MentalHealthAPI.Models
{
    public class Therapist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public List<Appointment> Appointments { get; set; }
        public string About { get; set; }
        public string Experience { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}
