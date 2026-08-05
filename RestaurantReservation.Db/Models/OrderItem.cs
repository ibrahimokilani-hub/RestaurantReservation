using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Models;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int ItemId { get; set; }
    public MenuItem MenuItem { get; set; } = null!;

    public int Quantity { get; set; }
}
