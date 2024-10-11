using CustomersRepo.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CustomersRepo.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomersAsync(string userId);
        Task<bool> UpdateCustomersAsync(string userId, List<Customer> customers);
        Task<bool> DeleteCustomersAsync(string userId, List<int> customerIds);
    }
}
