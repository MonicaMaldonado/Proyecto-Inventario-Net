using Microsoft.EntityFrameworkCore;
using TransaccionApi;
using TransaccionApi.Endpoints;
using TransaccionApi.Repository;
using TransaccionApi.Service;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ITransaccionService, TransaccionService>();
builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>();


builder.Services.AddHttpClient();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AirbnbClone API V1");
    });
}

app.UseHttpsRedirection();

app.MapTransaccionEndpoints();

app.Run();


