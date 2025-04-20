using Microsoft.AspNetCore.Mvc;

namespace MentalHealthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        // Hardcoded resources for now; replace with database in the future
        private readonly List<object> _resources = new()
        {
            new { Id = 1, Title = "Coping with Stress in College", Author = "J. Rowling", Date = "March 2024" },
            new { Id = 2, Title = "Work Stress Management Techniques", Author = "E. Nolan", Date = "Jan 2024" },
            new { Id = 3, Title = "Improving Your Mental Well-Being", Author = "A. Smith", Date = "May 2023" }
        };

        // Get all self-help resources
        [HttpGet]
        public IActionResult GetResources()
        {
            return Ok(_resources);
        }
    }
}
