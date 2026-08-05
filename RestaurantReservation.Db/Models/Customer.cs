using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(50)]
    public string LastName { get; set; } = null!;

    [MaxLength(255)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    public ICollection<Reservation> Reservations { get; set; } = [];
}
