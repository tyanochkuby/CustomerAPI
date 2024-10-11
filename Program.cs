using CustomersRepo;
using CustomersRepo.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();
app.Urls.Add("https://localhost:5001");

app.MapUserEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIdentityApi<User>();


app.MapControllers();

app.Run();
