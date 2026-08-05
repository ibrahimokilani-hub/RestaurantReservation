using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Db.Models;

public class Restaurant
{
    [Key]
    public int RestaurantId { get; set; }
    [Required]
    public string Name { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Address { get; set; }
    [Column(TypeName =  "varchar(150)")]
    public string PhoneNumber { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string OpeningHours { get; set; }
    
    public ICollection<Table> Tables { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
    public ICollection<Employee> Employees { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; }
}