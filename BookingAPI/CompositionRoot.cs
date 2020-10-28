using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace BookingAPI
{
    public class CompositionRoot : IControllerActivator
    {
        public TimeSpan SeatingDuration { get; }
        public IReadOnlyCollection<Table> Tables { get; }
        public string ConnectionString { get; }
        public ConcurrentDictionary<object, ScopedLog> Logs { get; }
        public FileInfo LogFile { get; }

        public CompositionRoot(
            TimeSpan seatingDuration,
            IReadOnlyCollection<Table> tables,
            string connectionString,
            ConcurrentDictionary<object, ScopedLog> logs,
            FileInfo logFile)
        {
            SeatingDuration = seatingDuration;
            Tables = tables;
            ConnectionString = connectionString;
            Logs = logs;
            LogFile = logFile;
        }

        public object Create(ControllerContext context)
        {
            var controllerType = context.ActionDescriptor.ControllerTypeInfo.AsType();

            if (controllerType == typeof(ReservationsController))
            {
                var l = new ScopedLog(new FileLog(LogFile));
                var controller = new ReservationsController(
                    SeatingDuration,
                    Tables,
                    new LogReservationsRepository(
                        new SqlReservationsRepository(ConnectionString),
                        l),
                    new LogClock(
                        new SystemClock(),
                        l));
                Logs.AddOrUpdate(controller, l, (_, x) => x);
                return controller;
            }
            throw new InvalidOperationException(
                $"Unknown controller type: {controllerType}.");
        }

        public void Release(ControllerContext context, object  controller)
        {

        }
    }

    
}
