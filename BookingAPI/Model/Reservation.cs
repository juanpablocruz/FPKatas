using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
