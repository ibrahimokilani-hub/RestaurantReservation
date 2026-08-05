using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Db.Models;

public class Reservation
{
    [Key]
    public int ReservationId { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime ReservationDate { get; set; }

    public int PartySize { get; set; }

    public int TableId { get; set; }
    public Table Table { get; set; } = null!;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = [];
}
