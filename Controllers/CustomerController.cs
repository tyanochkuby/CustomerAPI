using CustomersRepo.Data.Interfaces;
using CustomersRepo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomerController : Controller, ICustomerService
{
    private readonly CustomersDbContext _context;
    private static DateTime _lastUpdateSent = DateTime.MinValue;
    private static readonly TimeSpan DeadbandDuration = TimeSpan.FromMilliseconds(500);

    public CustomerController(CustomersDbContext context)
    {
        _context = context;
    }

    [HttpGet("get")]
    public async Task<List<Customer>> GetCustomersAsync()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get logged-in user's ID

        return await _context.Customers
            .Where(c => c.UserId == userId) // Only return customers belonging to this user
            .ToListAsync();
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCustomersAsync([FromBody] List<Customer> customers)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        foreach (var customer in customers)
        {
            if (customer.UserId != userId)
            {
                return Forbid(); // Prevent users from modifying other users' customers
            }
        }

        _context.Customers.UpdateRange(customers);
        await _context.SaveChangesAsync();

        return Ok(customers);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteCustomersAsync([FromQuery] List<int> customerIds)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var customersToDelete = await _context.Customers
                .Where(c => customerIds.Contains(c.Id) && c.UserId == userId)
                .ToListAsync();

            if (!customersToDelete.Any())
            {
                return NotFound(); // No customers found for the logged-in user
            }

            _context.Customers.RemoveRange(customersToDelete);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return Ok();
        }
        catch
        {
            await transaction.RollbackAsync();
            return BadRequest();
        }
    }
}
