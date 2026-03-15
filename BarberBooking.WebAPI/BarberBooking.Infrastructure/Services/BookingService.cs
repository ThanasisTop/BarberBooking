using BarberBooking.Application.Interfaces;
using BarberBooking.Application.Interfaces.Services;
using BarberBooking.Core.Entities;
using BarberBooking.Core.Entities.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Booking> _genericRepository;
        public BookingService(IGenericRepository<Booking> genericRepository) {
            _genericRepository = genericRepository;
        }

        public async Task<Result<Booking>> AddBookingAsync(Booking booking)
        {
            booking.Status = "Pending";
            try 
            {
                var existingBooking = await _genericRepository.GetAllAsync(b => b.PersonId == booking.PersonId && 
                                                                           b.BookingDate == booking.BookingDate && 
                                                                           b.Time == booking.Time);
                if (existingBooking.Any())
                {
                    return Result<Booking>.Failure("A booking with these details already exists.");
                }

                var createdBooking = await _genericRepository.AddAsync(booking);
                return Result<Booking>.Success(createdBooking);
            }
            catch (DbUpdateException ex) when (IsUniqueIndexViolation(ex)) //Concurency check for unique index violation
            {
                return Result<Booking>.Failure("A booking with these details already exists.");
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                return Result<Booking>.Failure($"An error occurred while adding the booking: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<Booking>>> GetAllBookingsAsync()
        {
            try
            {
                
                var bookingToReturn = await _genericRepository.GetAllAsync(b=>true);
                return Result<IEnumerable<Booking>>.Success(bookingToReturn);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                // Return an empty list or handle it as per your application's requirements

                return Result<IEnumerable<Booking>>.Failure($"An error occurred while retrieving bookings: {ex.Message}");
            }
        }

        public Task<Result<Booking>> GetBookingByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Booking>> UpdateBookingAsync(Booking booking)
        {
            try {
                var updatedBooking = await _genericRepository.UpdateAsync(booking);

                return Result<Booking>.Success(updatedBooking);
            } catch (Exception ex)
            {
                return Result<Booking>.Failure($"An error occurred while updating the booking: {ex.Message}");
            }
        }

        private bool IsUniqueIndexViolation(DbUpdateException ex)
        {
            return ex.InnerException?.Message.Contains("IX_Bookings_PersonId_BookingDate_Time") == true;
        }
    }
}
