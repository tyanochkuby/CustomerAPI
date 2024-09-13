using Microsoft.AspNetCore.Identity;

namespace CustomersRepo.Data
{
    public class User : IdentityUser
    {
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
