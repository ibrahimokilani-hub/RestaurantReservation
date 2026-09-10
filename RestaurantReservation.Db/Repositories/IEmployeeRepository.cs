using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> ListAllManagersAsync();

    Task<Employee?> GetByIdAsync(int employeeId);

    Task<bool> ExistsAsync(int employeeId);

    /// <summary>
    /// Average <see cref="Order.TotalAmount"/> across every order the employee handled,
    /// or <c>null</c> when the employee has handled no orders.
    /// </summary>
    Task<decimal?> GetAverageOrderAmountAsync(int employeeId);

    /// <summary>Number of orders the employee handled, so callers can report an honest zero.</summary>
    Task<int> CountOrdersAsync(int employeeId);
}
