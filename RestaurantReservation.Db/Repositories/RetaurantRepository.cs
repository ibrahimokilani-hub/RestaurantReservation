using System.Formats.Asn1;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Context;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class RestaurantRepository(RestaurantReservationDbContext context) : IRestaurantRepository
{
    public async Task<List<Restaurant>> GetAllAsync()
    {
        return await context.Restaurants.ToListAsync();
    }

    public async Task<bool> CreateAsync(Restaurant restaurant)
    {
        context.Restaurants.Add(restaurant);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(Restaurant restaurant)
    {
        var existing = await context.Restaurants.FindAsync(restaurant.RestaurantId);

        if (existing is null) return false;
        context.Entry(existing).CurrentValues.SetValues(restaurant);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int restaurantId)
    {
        var existing = await context.Restaurants.FirstOrDefaultAsync(res => res.RestaurantId == restaurantId);
        if (existing is null) return false;
        context.Restaurants.Remove(existing);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> HasExistingReservationsAsync(int restaurantId)
    {
        return await context.Restaurants.AnyAsync(res => res.RestaurantId == restaurantId);
    }
}