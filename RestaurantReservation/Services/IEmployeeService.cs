using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public interface IEmployeeService
{
    Task<List<Employee>> ListAllManagersAsync();
}