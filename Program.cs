using CustomersRepo.Data.Interfaces;
using CustomersRepo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
builder.Services.AddAuthorization();

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<CustomersDbContext>()
    .AddApiEndpoints();



builder.Services.AddDbContext<CustomersDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Customers")));

var app = builder.Build();
app.Urls.Add("https://localhost:5001");


app.MapGet("users/me", async (ClaimsPrincipal claims, CustomersDbContext dbContext) =>
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

app.MapPut("setDefaultLightMode", async (ClaimsPrincipal claims, bool lightMode, CustomersDbContext dbContext) =>
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



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIdentityApi<User>();


app.MapControllers();

app.Run();
