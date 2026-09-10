using RestaurantReservation.API.Dtos;
using RestaurantReservation.API.Exceptions;
using RestaurantReservation.API.Mapping;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Services;

/// <summary>
/// Reservation policy: referential checks, seating rules and not-found/conflict decisions
/// live here so the endpoints stay thin and the repository stays purely about persistence.
/// </summary>
public class ReservationService : IReservationService
{
    private readonly IReservationRepository _repository;

    public ReservationService(IReservationRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ReservationDto>> GetAllAsync()
    {
        var reservations = await _repository.GetAllAsync();
        return reservations.Select(r => r.ToDto()).ToList();
    }

    public async Task<ReservationDto> GetByIdAsync(int reservationId)
    {
        var reservation = await _repository.GetByIdAsync(reservationId)
            ?? throw NotFoundException.For("Reservation", reservationId);

        return reservation.ToDto();
    }

    public async Task<ReservationDto> CreateAsync(ReservationCreateDto request)
    {
        if (request.ReservationDate <= DateTime.UtcNow)
        {
            throw new BusinessRuleException(
                "The reservation date must be in the future.");
        }

        await EnsureReferencesExistAsync(request.CustomerId, request.TableId, request.RestaurantId);
        await EnsureTableFitsPartyAsync(request.TableId, request.PartySize);

        var created = await _repository.CreateAsync(new Reservation
        {
            ReservationDate = request.ReservationDate,
            PartySize = request.PartySize,
            TableId = request.TableId,
            CustomerId = request.CustomerId,
            RestaurantId = request.RestaurantId
        });

        // Re-read so the response carries the customer name like every other read does.
        var reloaded = await _repository.GetByIdAsync(created.ReservationId);
        return (reloaded ?? created).ToDto();
    }

    public async Task<ReservationDto> UpdateAsync(int reservationId, ReservationUpdateDto request)
    {
        if (!await _repository.ExistsAsync(reservationId))
        {
            throw NotFoundException.For("Reservation", reservationId);
        }

        await EnsureReferencesExistAsync(request.CustomerId, request.TableId, request.RestaurantId);
        await EnsureTableFitsPartyAsync(request.TableId, request.PartySize);

        var updated = await _repository.UpdateAsync(new Reservation
        {
            ReservationId = reservationId,
            ReservationDate = request.ReservationDate,
            PartySize = request.PartySize,
            TableId = request.TableId,
            CustomerId = request.CustomerId,
            RestaurantId = request.RestaurantId
        });

        if (!updated)
        {
            throw NotFoundException.For("Reservation", reservationId);
        }

        var reloaded = await _repository.GetByIdAsync(reservationId)
            ?? throw NotFoundException.For("Reservation", reservationId);

        return reloaded.ToDto();
    }

    public async Task DeleteAsync(int reservationId)
    {
        if (!await _repository.ExistsAsync(reservationId))
        {
            throw NotFoundException.For("Reservation", reservationId);
        }

        // Orders cascade from reservations at the database level. Refuse rather than
        // silently destroying order history.
        if (await _repository.HasOrdersAsync(reservationId))
        {
            throw new ConflictException(
                $"Reservation {reservationId} has orders attached and cannot be deleted. " +
                "Remove its orders first.");
        }

        if (!await _repository.DeleteAsync(reservationId))
        {
            throw NotFoundException.For("Reservation", reservationId);
        }
    }

    public async Task<List<ReservationDto>> GetByCustomerAsync(int customerId)
    {
        if (!await _repository.CustomerExistsAsync(customerId))
        {
            throw NotFoundException.For("Customer", customerId);
        }

        var reservations = await _repository.GetReservationsByCustomer(customerId);
        return reservations.Select(r => r.ToDto()).ToList();
    }

    public async Task<List<OrderWithMenuItemsDto>> GetOrdersAsync(int reservationId)
    {
        await EnsureReservationExistsAsync(reservationId);

        var orders = await _repository.GetOrdersWithMenuItemsAsync(reservationId);
        return orders.Select(o => o.ToDto()).ToList();
    }

    public async Task<List<MenuItemDto>> GetOrderedMenuItemsAsync(int reservationId)
    {
        await EnsureReservationExistsAsync(reservationId);

        var menuItems = await _repository.GetOrderedMenuItemsAsync(reservationId);
        return menuItems.Select(mi => mi.ToDto()).ToList();
    }

    private async Task EnsureReservationExistsAsync(int reservationId)
    {
        if (!await _repository.ExistsAsync(reservationId))
        {
            throw NotFoundException.For("Reservation", reservationId);
        }
    }

    private async Task EnsureReferencesExistAsync(int customerId, int tableId, int restaurantId)
    {
        if (!await _repository.CustomerExistsAsync(customerId))
        {
            throw new BusinessRuleException($"Customer {customerId} does not exist.");
        }

        if (!await _repository.TableExistsAsync(tableId))
        {
            throw new BusinessRuleException($"Table {tableId} does not exist.");
        }

        if (!await _repository.RestaurantExistsAsync(restaurantId))
        {
            throw new BusinessRuleException($"Restaurant {restaurantId} does not exist.");
        }
    }

    private async Task EnsureTableFitsPartyAsync(int tableId, int partySize)
    {
        var capacity = await _repository.GetTableCapacityAsync(tableId);

        if (capacity is not null && partySize > capacity)
        {
            throw new BusinessRuleException(
                $"Table {tableId} seats {capacity} guests, which is fewer than the requested party of {partySize}.");
        }
    }
}
