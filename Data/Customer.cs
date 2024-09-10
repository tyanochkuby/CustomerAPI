using CustomersRepo.Data.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomersRepo.Data
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [PolishAlphabet]
        public required string FirstName { get; set; }

        [PolishAlphabet]
        public required string LastName { get; set; }

        [PolishAlphanumeric]
        public required string StreetName { get; set; }

        [HouseNumber]
        public required string HouseNumber { get; set; }

        [AppartmentNumber]
        public string? AppartmentNumber { get; set; }

        [PostalCode]
        public required string PostalCode { get; set; }

        [PolishAlphabet]
        public required string Town { get; set; }

        [PhoneNumber]
        public required string PhoneNumber { get; set; }

        public required DateTime BirthDate { get; set; }
    }
}
