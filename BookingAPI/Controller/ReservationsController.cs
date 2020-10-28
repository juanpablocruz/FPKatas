using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookingAPI
{
    [ApiController, Route("[controller]")]
    public class ReservationsController : ControllerBase
    {
        /*
         * Maître is an object that implements the decision
         * logic as the _pure_ CanAccept function.
         */
        private readonly MaîtreD maîtreD;

        /*
         * Primitive dependencies
         */
        public TimeSpan SeatingDuration { get; }
        public IReadOnlyCollection<Table> Tables { get; }

        /* 
         * Clock and Repository are impure, therefore they are
         * injected dependencies
         */
        public IReservationsRepository Repository { get; }
        public IClock Clock { get; }

        public ReservationsController(
            TimeSpan seatingDuration,
            IReadOnlyCollection<Table> tables,
            IReservationsRepository repository,
            IClock clock)
        {
            SeatingDuration = seatingDuration;
            Tables = tables;
            Repository = repository;
            Clock = clock;
            maîtreD = new MaîtreD(seatingDuration, tables);
        }


        [HttpPost]
        public ActionResult Post(ReservationDto dto)
        {
            // Pure
            if (!DateTime.TryParse(dto.Date, out var _))
                return BadRequest($"Invalid date: {dto.Date}.");
            Reservation reservation = Mapper.Map(dto);

            // Impure
            if (reservation.Date < Clock.GetCurrentDateTime())
                return BadRequest($"Invalid date: {reservation.Date}.");
            var reservations = Repository.ReadReservations(reservation.Date);

            // Pure
            bool accepted = maîtreD.CanAccept(reservations, reservation);
            if (!accepted)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Couldn't accept.");
            
            // Impure
            Repository.Create(reservation);
            return Ok();
        }
    }
}
