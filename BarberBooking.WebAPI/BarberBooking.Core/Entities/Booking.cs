using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Core.Entities
{
    public class Booking : EntityBase
    {
        public Guid Id { get; set; }
        public Guid BarberId { get; set; }
        public DateTime BookingDate { get; set; }
        public string? Service { get; set; }
        public decimal Price { get; set; }

        public Barber Barber { get; set; }
    }
}
