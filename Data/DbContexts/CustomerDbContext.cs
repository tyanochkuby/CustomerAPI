using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CustomersRepo.Data.Entities;

namespace CustomersRepo.Data.DbContexts
{
    public class CustomersDbContext : IdentityDbContext<User>
    {
        public CustomersDbContext(DbContextOptions<CustomersDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
