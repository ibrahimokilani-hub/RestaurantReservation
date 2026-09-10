using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Dtos;
using RestaurantReservation.API.Services;
using RestaurantReservation.API.Validation;

namespace RestaurantReservation.API.Endpoints;

public static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/login", (
                [FromBody] LoginRequest request,
                ITokenService tokenService) =>
            {
                if (!tokenService.AreCredentialsValid(request.Username, request.Password))
                {
                    return Results.Problem(
                        detail: "The username or password provided is incorrect.",
                        statusCode: StatusCodes.Status401Unauthorized,
                        title: "Authentication failed");
                }

                var (token, expiresAtUtc) = tokenService.GenerateToken(request.Username);

                return Results.Ok(new LoginResponse
                {
                    AccessToken = token,
                    ExpiresAtUtc = expiresAtUtc
                });
            })
            .WithName("Login");

        return group;
    }
}
