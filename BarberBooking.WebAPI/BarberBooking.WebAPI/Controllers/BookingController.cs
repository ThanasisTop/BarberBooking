using BarberBooking.Application.Interfaces.Services;
using BarberBooking.Core.Entities;
using BarberBooking.WebAPI.Hubs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.SignalR;

namespace BarberBooking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        readonly IBookingService _bookingService;
        private readonly IHubContext<BookingHub> _hubContext;
        public BookingController(IBookingService bookingService, IHubContext<BookingHub> hubContext)
        {
            _bookingService = bookingService;
            _hubContext = hubContext;
        }

        [HttpPost]
        [EnableRateLimiting("booking-form-per-ip")]
        public async Task<IActionResult> AddBooking(Booking booking)
        {
            var addedBooking = await _bookingService.AddBookingAsync(booking);

            if(!string.IsNullOrEmpty(addedBooking.Error))
            {
                return BadRequest(addedBooking.Error);
            }
            
            await _hubContext.Clients.All.SendAsync("ReceiveBooking", addedBooking.Value);
            

            return Ok(addedBooking.Value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking(Booking booking)
        {
            var updatedBooking = await _bookingService.UpdateBookingAsync(booking);
            if (!string.IsNullOrEmpty(updatedBooking.Error))
            {
                return BadRequest(updatedBooking.Error);
            }

            return Ok(updatedBooking.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();

            if (!string.IsNullOrEmpty(bookings.Error))
            {
                return BadRequest(bookings.Error);
            }

            return Ok(bookings.Value);
        }

        [HttpGet("getPaged")]
        public async Task<IActionResult> GetPagedBookings(int pageNumber, int pageSize)
        {
            var bookings = await _bookingService.GetPagedBookingsAsync(pageNumber, pageSize);

            if (!string.IsNullOrEmpty(bookings.Error))
            {
                return BadRequest(bookings.Error);
            }

            return Ok(bookings.Value);
        }
    }
}
