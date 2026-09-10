using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Controller.API.Services;

namespace RestaurantReservation.Controller.API.Controllers;

[ApiController]
// [Authorize]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    [HttpGet]
    [Route("managers")]
    public async Task<IActionResult> GetManagers(IEmployeeService service)
    {
        return Ok(await service.ListAllManagersAsync());
    }

    [HttpGet("{employeeId:int}")]
    [Route("{employeeId:int}/average-order-amount")]
    public async Task<IActionResult> GetAverageOrderAmountforEmployee([FromRoute] int employeeId, IEmployeeService service)
    {
        return Ok(await service.GetAverageOrderAmountAsync(employeeId));
    }
}