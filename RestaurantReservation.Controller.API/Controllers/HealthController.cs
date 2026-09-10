using Microsoft.AspNetCore.Mvc;

namespace RestaurantReservation.Controller.API.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult GetHealth()
    {
        return Ok(new
        {
            status = "Healthy"
        });
    }
}