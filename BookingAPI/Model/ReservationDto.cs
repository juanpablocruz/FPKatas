using System;

namespace BookingAPI
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
