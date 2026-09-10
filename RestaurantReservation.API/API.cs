using RestaurantReservation.API.Endpoints;

namespace RestaurantReservation.API;

public static class API
{
    public static RouteGroupBuilder MapAPIEndpoints(this RouteGroupBuilder group)
    {
        group.MapGroup("/auth").MapAuthEndpoints();
        group.MapGroup("/reservations").MapReservationEndpoints();
        group.MapGroup("/employees").MapEmployeeEndpoints();

        return group;
    }
}
