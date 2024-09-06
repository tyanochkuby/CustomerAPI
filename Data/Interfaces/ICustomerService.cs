namespace CustomersRepo.Data.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<Customer>> GetCustomersAsync();
        public Task SetCustomersAsync(List<Customer> customers);

        public Task AddCustomerAsync(Customer customer);
    }
}
