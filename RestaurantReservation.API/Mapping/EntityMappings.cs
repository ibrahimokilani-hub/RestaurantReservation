using RestaurantReservation.API.Dtos;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mapping;

/// <summary>
/// Hand-written entity to DTO projections. Keeps EF entities out of the HTTP surface
/// without pulling in a mapping framework.
/// </summary>
public static class EntityMappings
{
    public static ReservationDto ToDto(this Reservation reservation) => new()
    {
        ReservationId = reservation.ReservationId,
        ReservationDate = reservation.ReservationDate,
        PartySize = reservation.PartySize,
        TableId = reservation.TableId,
        CustomerId = reservation.CustomerId,
        CustomerName = reservation.Customer is null
            ? null
            : $"{reservation.Customer.FirstName} {reservation.Customer.LastName}",
        RestaurantId = reservation.RestaurantId
    };

    public static EmployeeDto ToDto(this Employee employee) => new()
    {
        EmployeeId = employee.EmployeeId,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Position = employee.Position,
        RestaurantId = employee.RestaurantId
    };

    public static MenuItemDto ToDto(this MenuItem menuItem) => new()
    {
        ItemId = menuItem.ItemId,
        Name = menuItem.Name,
        Description = menuItem.Description,
        Price = menuItem.Price
    };

    public static OrderItemDto ToDto(this OrderItem orderItem) => new()
    {
        OrderItemId = orderItem.OrderItemId,
        ItemId = orderItem.ItemId,
        Name = orderItem.MenuItem?.Name ?? string.Empty,
        Description = orderItem.MenuItem?.Description,
        Price = orderItem.MenuItem?.Price ?? 0m,
        Quantity = orderItem.Quantity,
        LineTotal = (orderItem.MenuItem?.Price ?? 0m) * orderItem.Quantity
    };

    public static OrderWithMenuItemsDto ToDto(this Order order) => new()
    {
        OrderId = order.OrderId,
        OrderDate = order.OrderDate,
        TotalAmount = order.TotalAmount,
        EmployeeId = order.EmployeeId,
        Items = order.OrderItems.Select(oi => oi.ToDto()).ToList()
    };
}
