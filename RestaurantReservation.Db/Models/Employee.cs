using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Models;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(50)]
    public string LastName { get; set; } = null!;

    [MaxLength(50)]
    public string? Position { get; set; }

    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = [];
}
