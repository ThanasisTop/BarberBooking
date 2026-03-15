using BarberBooking.Core.Entities;
using BarberBooking.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<Result<Booking>> AddBookingAsync(Booking booking);

        Task<Result<IEnumerable<Booking>>> GetAllBookingsAsync();

        Task<Result<Booking>> UpdateBookingAsync(Booking booking);

        Task<Result<Booking>> GetBookingByIdAsync(Guid id);
    }
}
