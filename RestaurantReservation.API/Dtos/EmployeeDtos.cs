namespace RestaurantReservation.API.Dtos;

/// <summary>An employee as returned by the API.</summary>
public record EmployeeDto
{
    public int EmployeeId { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string? Position { get; init; }

    public int RestaurantId { get; init; }
}

/// <summary>Average value of the orders an employee has handled.</summary>
public record AverageOrderAmountDto
{
    public int EmployeeId { get; init; }
    
    public int OrderCount { get; init; }
    
    public decimal AverageOrderAmount { get; init; }
}
