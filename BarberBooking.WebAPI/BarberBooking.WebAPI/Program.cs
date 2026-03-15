using BarberBooking.Application.Interfaces;
using BarberBooking.Application.Interfaces.Services;
using BarberBooking.Application.Mapping;
using BarberBooking.Infrastructure.Persistence;
using BarberBooking.Infrastructure.Repositories;
using BarberBooking.Infrastructure.Services;
using BarberBooking.WebAPI.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPersonRepository,PersonRepository>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
