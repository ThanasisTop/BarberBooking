using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BarberBooking.Core.Entities
{
    public class Booking : EntityBase
    {
        
        public Guid? PersonId { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BookingDate { get; set; }
        public string? Time { get; set; }
        public string? Service { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }
        public string? Status { get; set; }

        [NotMapped]
        public int TotalBookings { get; set; }

        [NotMapped]
        public IEnumerable<Booking>? PagedBookings { get; set; }

        [JsonIgnore]
        public Person? Person { get; set; }
    }
}
