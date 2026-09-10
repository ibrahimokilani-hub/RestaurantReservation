namespace RestaurantReservation.Controller.API.Services;

public interface ITokenService
{
    /// <summary>Issues a signed bearer token for the given user.</summary>
    (string Token, DateTime ExpiresAtUtc) GenerateToken(string username);

    /// <summary>Checks the demo credentials configured under the <c>Auth</c> section.</summary>
    bool AreCredentialsValid(string username, string password);
}
