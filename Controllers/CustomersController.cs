using CustomersRepo.Data;
using CustomersRepo.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CustomersRepo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller, ICustomerService
    {
        private readonly CustomersDbContext _context;

        public CustomersController(CustomersDbContext context)
        {
            _context = context;
        }

        [HttpGet("get")]
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomersAsync([FromBody] List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                customer.Age = Customer.CalculateAge(customer.BirthDate);
            }

            await _context.Customers.AddRangeAsync(customers);
            await _context.SaveChangesAsync();

            return Ok(customers);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCustomersAsync([FromBody] List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                customer.Age = Customer.CalculateAge(customer.BirthDate);
            }

            _context.Customers.UpdateRange(customers);
            await _context.SaveChangesAsync();
            return Ok(customers);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteCustomersAsync(List<Guid> customerIds)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.Customers
                    .Where(x => customerIds.Contains(x.Id))
                    .ExecuteDeleteAsync();

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
}
