using RestaurantReservation.Controller.API.Dtos;
using RestaurantReservation.Controller.API.Exceptions;
using RestaurantReservation.Controller.API.Mapping;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Controller.API.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<EmployeeDto>> ListAllManagersAsync()
    {
        var managers = await _repository.ListAllManagersAsync();
        return managers.Select(e => e.ToDto()).ToList();
    }

    public async Task<AverageOrderAmountDto> GetAverageOrderAmountAsync(int employeeId)
    {
        if (!await _repository.ExistsAsync(employeeId))
        {
            throw NotFoundException.For("Employee", employeeId);
        }

        var orderCount = await _repository.CountOrdersAsync(employeeId);
        var average = orderCount == 0
            ? 0m
            : await _repository.GetAverageOrderAmountAsync(employeeId) ?? 0m;

        return new AverageOrderAmountDto
        {
            EmployeeId = employeeId,
            OrderCount = orderCount,
            AverageOrderAmount = Math.Round(average, 2, MidpointRounding.AwayFromZero)
        };
    }
}
