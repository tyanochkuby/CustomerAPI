using CustomersRepo.Data.Interfaces;
using CustomersRepo.Data;
using Microsoft.EntityFrameworkCore;
using CustomersRepo.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddDbContext<CustomersDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Customers")));

var app = builder.Build();
app.Urls.Add("https://localhost:5001");

app.MapHub<CustomerHub>("/customerHub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
