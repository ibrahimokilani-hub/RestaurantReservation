using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Models;

public class MenuItem
{
    [Key]
    public int ItemId { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }  
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}