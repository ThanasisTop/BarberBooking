using Microsoft.AspNetCore.SignalR;

namespace BarberBooking.WebAPI.Hubs
{
    public class BookingHub : Hub
    {
        //If the flow is Client → Hub method → SignalR → Clients then create custom method here.
        //Because in our business we have Controller → SignalR → Clients we dont need to create custom method here.
    }
}
