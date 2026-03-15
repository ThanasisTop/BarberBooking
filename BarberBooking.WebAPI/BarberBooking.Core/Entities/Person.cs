namespace BarberBooking.Core.Entities
{
    public class Person : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal Rating { get; set; }

        public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
    }
}
