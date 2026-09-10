using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Context;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly RestaurantReservationDbContext _ctx;

    public EmployeeRepository(RestaurantReservationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<Employee>> ListAllManagersAsync()
    {
        return await _ctx.Employees
            .AsNoTracking()
            .Where(emp => emp.Position == "Manager")
            .OrderBy(emp => emp.FirstName)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int employeeId)
    {
        return await _ctx.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(emp => emp.EmployeeId == employeeId);
    }

    public async Task<bool> ExistsAsync(int employeeId)
    {
        return await _ctx.Employees.AnyAsync(emp => emp.EmployeeId == employeeId);
    }

    public async Task<decimal?> GetAverageOrderAmountAsync(int employeeId)
    {
        // Projecting to decimal? keeps AverageAsync from throwing when the employee has no orders.
        return await _ctx.Orders
            .Where(o => o.EmployeeId == employeeId)
            .AverageAsync(o => (decimal?)o.TotalAmount);
    }

    public async Task<int> CountOrdersAsync(int employeeId)
    {
        return await _ctx.Orders.CountAsync(o => o.EmployeeId == employeeId);
    }
}
