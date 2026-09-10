namespace RestaurantReservation.API.Exceptions;

/// <summary>Base type for errors that map onto a specific HTTP status code.</summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }
}

/// <summary>The requested resource does not exist. Surfaces as 404.</summary>
public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public static NotFoundException For(string resource, int id) =>
        new($"{resource} with id {id} was not found.");
}

/// <summary>The request is well formed but references data that breaks a rule. Surfaces as 422.</summary>
public class BusinessRuleException : DomainException
{
    public BusinessRuleException(string message) : base(message)
    {
    }
}

/// <summary>The request conflicts with the current state of the resource. Surfaces as 409.</summary>
public class ConflictException : DomainException
{
    public ConflictException(string message) : base(message)
    {
    }
}
