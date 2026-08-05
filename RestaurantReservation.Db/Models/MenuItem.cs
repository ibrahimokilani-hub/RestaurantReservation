using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db.Models;

public class MenuItem
{
    [Key]
    public int ItemId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Precision(18, 2)]
    public decimal Price { get; set; }

    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
