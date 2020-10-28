using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;


namespace BookingAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var logs = new ConcurrentDictionary<object, ScopedLog>();
            var logFilter = new LogFilter(logs);
            services.AddMvc().AddMvcOptions(o => o.Filters.Add(logFilter));

            var seatingDuration = Configuration.GetValue<TimeSpan>("SeatingDuration");
            var tables = Configuration.GetSection("Tables")
                .Get<int[]>()
                .Select(i => new Table(i))
                .ToArray();

            var capacity = Configuration.GetValue<int>("Capacity");
            var connectionString = Configuration.GetConnectionString("Booking");

            var rootDir = Directory.GetParent(Environment.CurrentDirectory);
            var logFile = new FileInfo(Path.Combine(rootDir.FullName, "log.txt"));

            services.AddSingleton<IControllerActivator>(
                new CompositionRoot(
                    seatingDuration,
                    tables,
                    connectionString,
                    logs,
                    logFile));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
