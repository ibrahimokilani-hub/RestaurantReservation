using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Validation;

/// <summary>
/// Runs DataAnnotations over the <typeparamref name="T"/> argument of an endpoint and short-circuits
/// with an RFC 7807 validation problem when it fails, so handlers only ever see valid input.
/// </summary>
public class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var model = context.Arguments.OfType<T>().FirstOrDefault();

        if (model is null)
        {
            return TypedResults.Problem(
                detail: "A request body of the expected shape is required.",
                statusCode: StatusCodes.Status400BadRequest,
                title: "Missing request body");
        }

        var results = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);

        if (Validator.TryValidateObject(model, validationContext, results, validateAllProperties: true))
        {
            return await next(context);
        }

        var errors = results
            .SelectMany(
                result => result.MemberNames.DefaultIfEmpty(string.Empty),
                (result, member) => new { Member = member, result.ErrorMessage })
            .GroupBy(entry => entry.Member)
            .ToDictionary(
                group => group.Key,
                group => group
                    .Select(entry => entry.ErrorMessage ?? "The value provided is not valid.")
                    .ToArray());

        return TypedResults.ValidationProblem(errors, title: "One or more validation errors occurred.");
    }
}

public static class ValidationFilterExtensions
{
    /// <summary>Validates the endpoint's <typeparamref name="T"/> body argument before the handler runs.</summary>
    public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder) where T : class =>
        builder
            .AddEndpointFilter<ValidationFilter<T>>()
            .ProducesValidationProblem();
}
