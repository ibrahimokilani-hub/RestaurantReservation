using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Dtos;

/// <summary>Credentials exchanged for a bearer token.</summary>
public class LoginRequest
{
    [Required(ErrorMessage = "A username is required.")]
    [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
    public string Username { get; init; } = string.Empty;

    [Required(ErrorMessage = "A password is required.")]
    [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
    public string Password { get; init; } = string.Empty;
}

/// <summary>A freshly issued bearer token.</summary>
public record LoginResponse
{
    public string TokenType { get; init; } = "Bearer";

    public string AccessToken { get; init; } = string.Empty;

    public DateTime ExpiresAtUtc { get; init; }
}
