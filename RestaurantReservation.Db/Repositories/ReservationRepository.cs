using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Context;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly RestaurantReservationDbContext _ctx;

    public ReservationRepository(RestaurantReservationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<Reservation>> GetAllAsync()
    {
        return await _ctx.Reservations
            .AsNoTracking()
            .Include(r => r.Customer)
            .OrderBy(r => r.ReservationDate)
            .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(int reservationId)
    {
        return await _ctx.Reservations
            .AsNoTracking()
            .Include(r => r.Customer)
            .FirstOrDefaultAsync(r => r.ReservationId == reservationId);
    }

    public async Task<Reservation> CreateAsync(Reservation reservation)
    {
        _ctx.Reservations.Add(reservation);
        await _ctx.SaveChangesAsync();

        return reservation;
    }

    public async Task<bool> UpdateAsync(Reservation reservation)
    {
        var existing = await _ctx.Reservations.FindAsync(reservation.ReservationId);
        if (existing is null) return false;

        _ctx.Entry(existing).CurrentValues.SetValues(reservation);
        await _ctx.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int reservationId)
    {
        var existing = await _ctx.Reservations.FindAsync(reservationId);
        if (existing is null) return false;

        _ctx.Reservations.Remove(existing);
        await _ctx.SaveChangesAsync();

        return true;
    }

    public async Task<List<Reservation>> GetReservationsByCustomer(int customerId)
    {
        return await _ctx.Reservations
            .AsNoTracking()
            .Where(reservation => reservation.CustomerId == customerId)
            .Include(r => r.Customer)
            .OrderBy(r => r.ReservationDate)
            .ToListAsync();
    }

    public async Task<List<Order>> GetOrdersWithMenuItemsAsync(int reservationId)
    {
        return await _ctx.Orders
            .AsNoTracking()
            .Where(o => o.ReservationId == reservationId)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)
            .OrderBy(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<List<MenuItem>> GetOrderedMenuItemsAsync(int reservationId)
    {
        return await _ctx.OrderItems
            .AsNoTracking()
            .Where(oi => oi.Order.ReservationId == reservationId)
            .Select(oi => oi.MenuItem)
            .Distinct()
            .OrderBy(mi => mi.Name)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(int reservationId)
    {
        return await _ctx.Reservations.AnyAsync(r => r.ReservationId == reservationId);
    }

    public async Task<bool> HasOrdersAsync(int reservationId)
    {
        return await _ctx.Orders.AnyAsync(o => o.ReservationId == reservationId);
    }

    public async Task<bool> CustomerExistsAsync(int customerId)
    {
        return await _ctx.Customers.AnyAsync(c => c.CustomerId == customerId);
    }

    public async Task<bool> TableExistsAsync(int tableId)
    {
        return await _ctx.Tables.AnyAsync(t => t.TableId == tableId);
    }

    public async Task<bool> RestaurantExistsAsync(int restaurantId)
    {
        return await _ctx.Restaurants.AnyAsync(r => r.RestaurantId == restaurantId);
    }

    public async Task<int?> GetTableCapacityAsync(int tableId)
    {
        return await _ctx.Tables
            .Where(t => t.TableId == tableId)
            .Select(t => (int?)t.Capacity)
            .FirstOrDefaultAsync();
    }
}
