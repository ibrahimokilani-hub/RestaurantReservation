namespace RestaurantReservation.Controller.API.Dtos;

/// <summary>A single menu item on an order, with the quantity ordered.</summary>
public record OrderItemDto
{
    public int OrderItemId { get; init; }

    public int ItemId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string? Description { get; init; }

    public decimal Price { get; init; }

    public int Quantity { get; init; }

    public decimal LineTotal { get; init; }
}

/// <summary>An order placed against a reservation, including its menu items.</summary>
public record OrderWithMenuItemsDto
{
    public int OrderId { get; init; }

    public DateTime OrderDate { get; init; }

    /// <example>128.75</example>
    public decimal TotalAmount { get; init; }

    public int EmployeeId { get; init; }

    public IReadOnlyList<OrderItemDto> Items { get; init; } = [];
}

/// <summary>A menu item.</summary>
public record MenuItemDto
{
    public int ItemId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string? Description { get; init; }

    public decimal Price { get; init; }
}
