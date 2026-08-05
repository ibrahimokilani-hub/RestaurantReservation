using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Db.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    [Column(TypeName = "DateTime2"), Required]
    public DateTime OrderDate { get; set; } 
    [Required]
    public decimal TotalAmount { get; set; } 
    
    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; }
    
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; } = [];

}