using BarberBooking.Core.Entities;

namespace BarberBooking.Application.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> GetPagedBookings(int pageNumber, int pageSize);
    }
}
