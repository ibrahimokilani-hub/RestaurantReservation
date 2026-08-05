using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Conext;

public class RestaurantReservationDbContext : DbContext                                               
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => oi.OrderItemId);

        modelBuilder.Entity<OrderItem>()
            .HasIndex(oi => new { oi.OrderId, oi.ItemId })
            .IsUnique();

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.MenuItem)
            .WithMany(mi => mi.OrderItems)
            .HasForeignKey(oi => oi.ItemId);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1434;Database=RestaurantReservationCore;User Id=kilani;Password=Nevigate@123;TrustServerCertificate=True;").UseSnakeCaseNamingConvention();
    }
}