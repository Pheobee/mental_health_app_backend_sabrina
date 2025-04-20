namespace MentalHealthAPI.Models
{
    public class AssessmentResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TestType { get; set; }
        public int Score { get; set; }
        public string Interpretation { get; set; }
        public DateTime Date { get; set; }
    }

}
