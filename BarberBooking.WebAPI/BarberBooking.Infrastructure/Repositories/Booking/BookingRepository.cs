using BarberBooking.Application.Interfaces;
using BarberBooking.Core.Entities;
using BarberBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberBooking.Infrastructure.Repositories
{
    public class BookingRepository :GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Booking> GetPagedBookings(int pageNumber, int pageSize)
        {
            var pagedBookings= await _context.Bookings
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalBookings = await _context.Bookings.CountAsync();

            return new Booking
            {
                PagedBookings = pagedBookings,
                TotalBookings = totalBookings
            };
        }
    }
}
