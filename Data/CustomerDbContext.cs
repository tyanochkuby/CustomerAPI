using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CustomersRepo.Data
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
            //builder.HasDefaultSchema("CustomersRepo");
            //builder.Entity<Customer>()
            //   .HasOne(c => c.User)
            //   .WithMany(u => u.Customers)
            //   .HasForeignKey(c => c.UserId);
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
