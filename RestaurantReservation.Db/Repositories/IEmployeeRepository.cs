using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public interface IEmployeeRepository
{
    public Task<List<Employee>> ListAllManagersAsync(); 
}