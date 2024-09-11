using CustomersRepo.Data;
using CustomersRepo.Data.Interfaces;
using CustomersRepo.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CustomersRepo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller, ICustomerService
    {
        private readonly CustomersDbContext _context;
        private readonly IHubContext<CustomerHub> _hubContext;
        private static DateTime _lastUpdateSent = DateTime.MinValue;
        private static readonly TimeSpan DeadbandDuration = TimeSpan.FromMilliseconds(500);


        public CustomersController(IHubContext<CustomerHub> hubContext, CustomersDbContext context)
        {
            _hubContext = hubContext;
            _context = context;
        }

        [HttpGet("get")]
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomersAsync([FromBody] List<Customer> customers)
        {
            _context.Customers.UpdateRange(customers);
            await _context.SaveChangesAsync();

            if (DateTime.UtcNow - _lastUpdateSent > DeadbandDuration)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveCustomerUpdate");
                _lastUpdateSent = DateTime.UtcNow;
            }

            return Ok(customers);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCustomersAsync([FromQuery] List<int> customerIds)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.Customers
                    .Where(x => customerIds.Contains(x.Id))
                    .ExecuteDeleteAsync();

                await transaction.CommitAsync();
                if (DateTime.UtcNow - _lastUpdateSent > DeadbandDuration)
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveCustomerUpdate");
                    _lastUpdateSent = DateTime.UtcNow;
                }
                return Ok();
            }
            catch
            {
                await transaction.RollbackAsync();
                return BadRequest();
            }
        }
    }
}
