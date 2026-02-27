using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Core.Entities
{
    public class Barber : EntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal Rating { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
