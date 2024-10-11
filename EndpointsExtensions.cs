using CustomersRepo.Data.DbContexts;

namespace CustomersRepo
{
    public static class EndpointExtensions
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("users/me", async (ClaimsPrincipal claims, CustomersDbContext dbContext) =>
            {
                string userId = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var user = await dbContext.Users.FindAsync(userId);
                return new
                {
                    userId = userId,
                    lightMode = user.LightMode
                };
            })
            .RequireAuthorization();

            endpoints.MapPut("setDefaultLightMode", async (ClaimsPrincipal claims, bool lightMode, CustomersDbContext dbContext) =>
            {
                string userId = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var user = await dbContext.Users.FindAsync(userId);
                if (user != null)
                {
                    user.LightMode = lightMode;
                    await dbContext.SaveChangesAsync();
                    return Results.Ok();
                }
                return Results.NotFound();
            })
            .RequireAuthorization();

            return endpoints;
        }
    }

}
