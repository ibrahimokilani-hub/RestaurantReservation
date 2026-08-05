using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Db.Models;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string Position { get; set; }
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    
    public ICollection<Order> Orders { get; set; }  
}