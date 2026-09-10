using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Controller.API.Dtos;

/// <summary>A reservation as returned by the API.</summary>
public record ReservationDto
{
    public int ReservationId { get; init; }

    public DateTime ReservationDate { get; init; }

    public int PartySize { get; init; }

    public int TableId { get; init; }

    public int CustomerId { get; init; }

    public string? CustomerName { get; init; }

    public int RestaurantId { get; init; }
}

/// <summary>Payload for creating a reservation.</summary>
public class ReservationCreateDto
{
    [Required(ErrorMessage = "A reservation date is required.")]
    public DateTime ReservationDate { get; init; }

    [Range(1, 100, ErrorMessage = "Party size must be between 1 and 100 guests.")]
    public int PartySize { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "TableId must be a positive number.")]
    public int TableId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be a positive number.")]
    public int CustomerId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "RestaurantId must be a positive number.")]
    public int RestaurantId { get; init; }
}

/// <summary>Payload for replacing an existing reservation.</summary>
public class ReservationUpdateDto
{
    [Required(ErrorMessage = "A reservation date is required.")]
    public DateTime ReservationDate { get; init; }

    [Range(1, 100, ErrorMessage = "Party size must be between 1 and 100 guests.")]
    public int PartySize { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "TableId must be a positive number.")]
    public int TableId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be a positive number.")]
    public int CustomerId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "RestaurantId must be a positive number.")]
    public int RestaurantId { get; init; }
}
