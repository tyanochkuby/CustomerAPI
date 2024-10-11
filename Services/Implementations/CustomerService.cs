using CustomersRepo.Data.DbContexts;
using CustomersRepo.Data.Entities;
using CustomersRepo.Services.Interfaces;

namespace CustomersRepo.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomersDbContext _context;

        public CustomerService(CustomersDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetCustomersAsync(string userId)
        {
            return await _context.Customers
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> UpdateCustomersAsync(string userId, List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                if (customer.UserId != userId)
                {
                    return false;
                }
            }

            _context.Customers.UpdateRange(customers);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCustomersAsync(string userId, List<int> customerIds)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var customersToDelete = await _context.Customers
                    .Where(c => customerIds.Contains(c.Id) && c.UserId == userId)
                    .ToListAsync();

                if (!customersToDelete.Any())
                {
                    return false;
                }

                _context.Customers.RemoveRange(customersToDelete);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
