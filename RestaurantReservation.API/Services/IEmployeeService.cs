using RestaurantReservation.API.Dtos;

namespace RestaurantReservation.API.Services;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> ListAllManagersAsync();

    Task<AverageOrderAmountDto> GetAverageOrderAmountAsync(int employeeId);
}
