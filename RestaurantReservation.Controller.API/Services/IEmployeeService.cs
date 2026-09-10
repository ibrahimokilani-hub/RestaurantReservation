using RestaurantReservation.Controller.API.Dtos;

namespace RestaurantReservation.Controller.API.Services;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> ListAllManagersAsync();

    Task<AverageOrderAmountDto> GetAverageOrderAmountAsync(int employeeId);
}
