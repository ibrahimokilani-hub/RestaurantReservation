using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime OrderDate { get; set; }

    [Precision(18, 2)]
    public decimal TotalAmount { get; set; }

    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; } = null!;

    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
