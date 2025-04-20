namespace MentalHealthAPI.DTO
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; } // Optional: For refresh token logic
        public DateTime Expiry { get; set; }
    }
}
