using CustomersRepo.Data.DbContexts;
using CustomersRepo.Data.Entities;
using CustomersRepo.Services.Implementations;
using CustomersRepo.Services.Interfaces;

namespace CustomersRepo
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSignalR();
            services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
            services.AddAuthorization();

            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<CustomersDbContext>()
                .AddApiEndpoints();

            services.AddDbContext<CustomersDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("Customers")));

            return services;
        }
    }

}
