using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public interface IReservationRepository
{
    public Task<List<Reservation>> GetReservationsByCustomer(int customerId);
}