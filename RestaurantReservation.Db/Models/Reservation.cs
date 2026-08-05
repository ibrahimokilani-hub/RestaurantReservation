using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Db.Models;

public class Reservation
{
    [Key]
    public int ReservationId { get; set; }
    
    [Column(TypeName = "DateTime2"), Required]
    public DateTime ReservationDate { get; set; } 
    
    [Required]
    public int PartySize { get; set; }
    
    public int TableId { get; set; }
    public Table Table { get; set; }
    
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    
    public ICollection<Order>  Orders { get; set; }
}