using System;
using System.Collections.Generic;
using System.Linq;

namespace FPKatas.MaîtreD.Solution
{
    public class MaîtreD
    {
        public TimeSpan SeatingDuration { get; }
        public IReadOnlyCollection<Table> Tables { get; }

        public MaîtreD(
            TimeSpan seatingDuration,
            IReadOnlyCollection<Table> tables) :
            this(seatingDuration, tables.ToArray())
        {

        }

        public MaîtreD(TimeSpan seatingDuration, params Table[] tables)
        {
            Tables = tables.OrderBy(t => t.Seats).ToArray();
            SeatingDuration = seatingDuration;
        }

        public bool CanAccept(
            IEnumerable<Reservation> reservations,
            Reservation reservation)
        {
            var remainingTables = Tables.ToList();
            var relevantReservations = reservations
                .Where(r => Overlaps(reservation, r))
                .OrderBy(r => r.Quantity);

            foreach (var r in relevantReservations)
            {
                var idx = remainingTables.FindIndex(t => r.Quantity <= t.Seats);
                if (idx < 0)
                    return false;
                remainingTables.RemoveAt(idx);
            }

            return remainingTables.Any(t => reservation.Quantity <= t.Seats);
        }

        private bool Overlaps(Reservation candidate, Reservation existing)
        {
            var aSeatingDurationBefore = candidate.Date.Subtract(SeatingDuration);
            var aSeatingDurationAfter = candidate.Date.Add(SeatingDuration);
            return aSeatingDurationBefore < existing.Date
                && existing.Date < aSeatingDurationAfter;
        }
    }
}
