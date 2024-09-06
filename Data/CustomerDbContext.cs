using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CustomersRepo.Data
{
    public class CustomersDbContext : DbContext
    {
        public CustomersDbContext(DbContextOptions<CustomersDbContext> options)
           : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
