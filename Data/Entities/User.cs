using Microsoft.AspNetCore.Identity;

namespace CustomersRepo.Data.Entities
{
    public class User : IdentityUser
    {
        public virtual ICollection<Customer> Customers { get; set; }

        public bool LightMode { get; set; }
    }
}
