using Microsoft.EntityFrameworkCore;

namespace CustomersRepo.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CustomersDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<CustomersDbContext>>()))
            {
                if (context.Customers.Any())
                {
                    return;
                }

                context.Customers.AddRange(
                    new Customer()
                    {
                        FirstName = "Jan",
                        LastName = "Kowalski",
                        StreetName = "Kwiatowa",
                        HouseNumber = "1",
                        AppartmentNumber = "2",
                        PostalCode = "12-345",
                        Town = "Warszawa",
                        PhoneNumber = "123456789",
                        BirthDate = new DateTime(1998, 04, 30)
                    },
                    new Customer()
                    {
                        FirstName = "Jana",
                        LastName = "Mazurska",
                        StreetName = "Ląkowa",
                        HouseNumber = "10a",
                        AppartmentNumber = "24",
                        PostalCode = "12-345",
                        Town = "Śrem",
                        PhoneNumber = "987654321",
                        BirthDate = new DateTime(1998, 04, 30)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
