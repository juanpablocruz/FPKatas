using System;

namespace BookingAPI
{
    public class Table
    {
        public int Seats { get; }
        public Table(int seats)
        {
            Seats = seats;
        }
        public override bool Equals(object obj)
        {
            return obj is Table table &&
                Seats == table.Seats;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Seats);
        }


    }
}
