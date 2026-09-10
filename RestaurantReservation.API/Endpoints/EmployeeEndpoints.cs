using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Dtos;
using RestaurantReservation.API.Services;

namespace RestaurantReservation.API.Endpoints;

public static class EmployeeEndpoints
{
    public static RouteGroupBuilder MapEmployeeEndpoints(this RouteGroupBuilder group)
    {
        group.RequireAuthorization();

        group.MapGet("/managers", async (IEmployeeService service) =>
                TypedResults.Ok(await service.ListAllManagersAsync()))
            .WithName("GetManagers");

        group.MapGet("/{employeeId:int}/average-order-amount", async (
                [FromRoute] int employeeId,
                IEmployeeService service) =>
                TypedResults.Ok(await service.GetAverageOrderAmountAsync(employeeId)))
            .WithName("GetEmployeeAverageOrderAmount");

        return group;
    }
}