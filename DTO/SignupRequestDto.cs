namespace MentalHealthAPI.DTO
{
    public class SignupRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Optional: Role for role-based authorization (User, Therapist, etc.)
    }
}
