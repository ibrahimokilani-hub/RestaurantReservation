using RestaurantReservation.Db.Context;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Services;

public class EmployeeService: IEmployeeService
{
    private readonly IEmployeeRepository _repo;

    public EmployeeService(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Employee>> ListAllManagersAsync()
    {
        return await  _repo.ListAllManagersAsync();
    }
}