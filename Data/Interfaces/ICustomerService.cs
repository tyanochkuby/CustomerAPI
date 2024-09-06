using Microsoft.AspNetCore.Mvc;

namespace CustomersRepo.Data.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<Customer>> GetCustomersAsync();

        public Task<IActionResult> UpdateCustomersAsync(List<Customer> customers);

        public Task<IActionResult> CreateCustomersAsync(List<Customer> customers);

        public Task<IActionResult> DeleteCustomersAsync(List<Guid> customerIds);
    }
}
