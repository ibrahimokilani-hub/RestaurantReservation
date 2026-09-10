using RestaurantReservation.API.Dtos;

namespace RestaurantReservation.API.Services;

public interface IReservationService
{
    Task<List<ReservationDto>> GetAllAsync();

    Task<ReservationDto> GetByIdAsync(int reservationId);

    Task<ReservationDto> CreateAsync(ReservationCreateDto request);

    Task<ReservationDto> UpdateAsync(int reservationId, ReservationUpdateDto request);

    Task DeleteAsync(int reservationId);

    Task<List<ReservationDto>> GetByCustomerAsync(int customerId);

    Task<List<OrderWithMenuItemsDto>> GetOrdersAsync(int reservationId);

    Task<List<MenuItemDto>> GetOrderedMenuItemsAsync(int reservationId);
}
