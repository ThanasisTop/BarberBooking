using BarberBooking.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Application.Validators
{
    public class BookingValidator:AbstractValidator<Booking>
    {
        public BookingValidator() { 
            RuleFor(b => b.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(b => b.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(b => b.Email)
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(b => b.Phone)
                .NotEmpty().WithMessage("Phone number is required.");

            RuleFor(b => b.Time)
                .NotEmpty().WithMessage("Time is required.");

            RuleFor(b => b.BookingDate)
                .NotEmpty().WithMessage("Booking date is required.");

            RuleFor(b => b.Service)
                .NotEmpty().WithMessage("Service is required.");
        }
    }
}
