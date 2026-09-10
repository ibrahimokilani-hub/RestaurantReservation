using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetAllAsync();
    Task<bool> CreateAsync(Restaurant restaurant);
    Task<bool> UpdateAsync(Restaurant restaurant);
    Task<bool> DeleteAsync(int restaurantId);
    
}