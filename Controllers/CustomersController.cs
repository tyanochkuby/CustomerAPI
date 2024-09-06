using CustomersRepo.Data;
using CustomersRepo.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("add")]
        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        [HttpGet("getall")]
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpPost("setall")]
        public async Task SetCustomersAsync(List<Customer> c)
        {
            foreach (var customer in c)
            {
                var existingCustomer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Id == customer.Id);

                if (existingCustomer == null)
                {
                    await _context.AddAsync(customer);
                }
                else
                {
                    _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
