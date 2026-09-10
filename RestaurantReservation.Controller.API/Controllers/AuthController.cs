using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Controller.API.Dtos;
using RestaurantReservation.Controller.API.Services;

namespace RestaurantReservation.Controller.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public IActionResult LoginController(
                [FromBody] LoginRequest request,
                ITokenService tokenService)
    {
        if (!tokenService.AreCredentialsValid(request.Username, request.Password))
        {
            return Problem(
                detail: "The username or password provided is incorrect.",
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Authentication failed");
        }

        var (token, expiresAtUtc) = tokenService.GenerateToken(request.Username);

        return Ok(new LoginResponse
        {
            AccessToken = token,
            ExpiresAtUtc = expiresAtUtc
        });
    }
}