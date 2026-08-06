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
    public async Task<List<Reservation>> GetReservationsByCustomer(int customerId)
    {
        return await _ctx.Reservations.Where(reservation => reservation.CustomerId == customerId).Include(r => r.Customer).ToListAsync();
    }
}