using BarberBooking.Application.Interfaces;
using BarberBooking.Application.Interfaces.Services;
using BarberBooking.Application.Mapping;
using BarberBooking.Application.Validators;
using BarberBooking.Core.Entities;
using BarberBooking.Infrastructure.Persistence;
using BarberBooking.Infrastructure.Repositories;
using BarberBooking.Infrastructure.Services;
using BarberBooking.WebAPI.Hubs;
using FluentValidation;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
//Services registrations
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IPersonService, PersonService>();

//Repository registrations
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPersonRepository,PersonRepository>();
builder.Services.AddScoped<IBookingRepository,BookingRepository>();

//Other registrations
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("booking-form-per-ip", httpContext =>
        RateLimitPartition.GetSlidingWindowLimiter(
            // Partition key = the requester's IP
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new SlidingWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                SegmentsPerWindow = 6,
                QueueLimit = 0
            }
        )
    );

    // What to return when rate limited
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.Headers["Retry-After"] = "60";

        await context.HttpContext.Response.WriteAsJsonAsync(new
        {
            error = "Too many requests. Please try again later."
        }, cancellationToken);
    };
});

builder.Services.AddValidatorsFromAssemblyContaining<BookingValidator>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(PersonProfile).Assembly);
builder.Services.AddControllers();
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.MapHub<BookingHub>("/bookingHub");

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.Run();
