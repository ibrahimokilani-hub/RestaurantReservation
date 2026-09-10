using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Dtos;
using RestaurantReservation.API.Services;
using RestaurantReservation.API.Validation;

namespace RestaurantReservation.API.Endpoints;

public static class ReservationEndpoints
{
    public static RouteGroupBuilder MapReservationEndpoints(this RouteGroupBuilder group)
    {
        group
            .RequireAuthorization()
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/", async (IReservationService service) =>
                TypedResults.Ok(await service.GetAllAsync()))
            .WithName("GetReservations");

        group.MapGet("/{reservationId:int}", async (
                int reservationId,
                IReservationService service) =>
                TypedResults.Ok(await service.GetByIdAsync(reservationId)))
            .WithName("GetReservationById");

        group.MapPost("/", async (
                ReservationCreateDto request,
                IReservationService service) =>
            {
                var created = await service.CreateAsync(request);
                return TypedResults.Created($"/api/reservations/{created.ReservationId}", created);
            })
            .WithName("CreateReservation");

        group.MapPut("/{reservationId:int}", async (
                int reservationId,
                ReservationUpdateDto request,
                IReservationService service) =>
                TypedResults.Ok(await service.UpdateAsync(reservationId, request)))
            .WithName("UpdateReservation");

        group.MapDelete("/{reservationId:int}", async (
                int reservationId,
                IReservationService service) =>
            {
                await service.DeleteAsync(reservationId);
                return TypedResults.NoContent();
            })
            .WithName("DeleteReservation");

        group.MapGet("/customer/{customerId:int}", async (
                int customerId,
                IReservationService service) =>
                TypedResults.Ok(await service.GetByCustomerAsync(customerId)))
            .WithName("GetReservationsByCustomer");

        group.MapGet("/{reservationId:int}/orders", async (
                int reservationId,
                IReservationService service) =>
                TypedResults.Ok(await service.GetOrdersAsync(reservationId)))
            .WithName("GetReservationOrders");

        group.MapGet("/{reservationId:int}/menu-items", async (
                int reservationId,
                IReservationService service) =>
                TypedResults.Ok(await service.GetOrderedMenuItemsAsync(reservationId)))
            .WithName("GetReservationMenuItems");

        return group;
    }
}
