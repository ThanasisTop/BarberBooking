using BarberBooking.Core.Entities;
using BarberBooking.Core.Entities.Common;

namespace BarberBooking.Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<Result<Booking>> AddBookingAsync(Booking booking);

        Task<Result<IEnumerable<Booking>>> GetAllBookingsAsync();

        Task<Result<Booking>> UpdateBookingAsync(Booking booking);

        Task<Result<Booking>> GetBookingByIdAsync(Guid id);

        Task<Result<Booking>> GetPagedBookingsAsync(int pageNumber, int pageSize);
    }
}
