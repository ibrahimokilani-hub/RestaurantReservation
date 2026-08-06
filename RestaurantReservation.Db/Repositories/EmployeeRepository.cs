using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Context;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class EmployeeRepository: IEmployeeRepository
{
    private readonly RestaurantReservationDbContext _ctx;
    public EmployeeRepository(RestaurantReservationDbContext ctx)
    {
        _ctx = ctx;
    }
    public async Task<List<Employee>> ListAllManagersAsync()
    {
        return await _ctx.Employees.Where(emp => emp.Position == "Manager").OrderBy(emp => emp.FirstName).ToListAsync();
    }
}