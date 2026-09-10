using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public interface IReservationRepository
{
    Task<List<Reservation>> GetAllAsync();

    Task<Reservation?> GetByIdAsync(int reservationId);

    Task<Reservation> CreateAsync(Reservation reservation);

    Task<bool> UpdateAsync(Reservation reservation);

    Task<bool> DeleteAsync(int reservationId);

    Task<List<Reservation>> GetReservationsByCustomer(int customerId);

    /// <summary>Orders placed against a reservation, with their order items and menu items.</summary>
    Task<List<Order>> GetOrdersWithMenuItemsAsync(int reservationId);

    /// <summary>Distinct menu items ordered across every order on a reservation.</summary>
    Task<List<MenuItem>> GetOrderedMenuItemsAsync(int reservationId);

    Task<bool> ExistsAsync(int reservationId);

    Task<bool> HasOrdersAsync(int reservationId);

    Task<bool> CustomerExistsAsync(int customerId);

    Task<bool> TableExistsAsync(int tableId);

    Task<bool> RestaurantExistsAsync(int restaurantId);

    /// <summary>Seats on the table a reservation is being booked against.</summary>
    Task<int?> GetTableCapacityAsync(int tableId);
}
