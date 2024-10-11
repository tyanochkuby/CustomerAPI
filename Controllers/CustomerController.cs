using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CustomersRepo.Data.Entities;
using CustomersRepo.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetCustomersAsync()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var customers = await _customerService.GetCustomersAsync(userId);

        return Ok(customers);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCustomersAsync([FromBody] List<Customer> customers)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var success = await _customerService.UpdateCustomersAsync(userId, customers);

        if (!success)
        {
            return Forbid();
        }

        return Ok(customers);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteCustomersAsync([FromQuery] List<int> customerIds)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var success = await _customerService.DeleteCustomersAsync(userId, customerIds);

        if (!success)
        {
            return NotFound();
        }

        return Ok();
    }
}
