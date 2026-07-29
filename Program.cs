using BusReservation.Api.Middleware;
using Microsoft.EntityFrameworkCore;
using BusReservation.Api.Data;
using BusReservation.Api.Services;
using BusReservation.Api.Options;
using BusReservation.Api.Exceptions;
using Scalar.AspNetCore;
using BusReservation.Api.Entities;


var builder = WebApplication.CreateBuilder(args);

// Register services

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddProblemDetails();

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IPassengerService,PassengerService>();
builder.Services.AddDbContext<BusReservationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("BusReservationDatabase"))
    .LogTo(Console.WriteLine, LogLevel.Information) // Show generated SQL
    .EnableSensitiveDataLogging());               // Show parameter values (Development only)


var app = builder.Build();


// Request logging should be early
app.UseMiddleware<RequestLoggingMiddleware>();


// Environment specific configuration
if (app.Environment.IsDevelopment())
{
    // OpenAPI + Scalar only in development
    app.MapOpenApi();

    app.MapScalarApiReference();
}
else
{
    // Hide stack traces in production
    app.UseExceptionHandler();
}


app.UseStatusCodePages();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapGet("/api/error", () =>
{
    throw new BookingDatabaseException(
        "Simulated booking database failure");
});


app.MapControllers();




app.Run();