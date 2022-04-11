using MayNazMuth.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayNazMuth.Utilities {
    class CustomDbContext : DbContext {
        //DB sets for the entities or tables created
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<BookingPassenger> BookingPassengers { get; set; }

        //Configuration of database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MayNazMuthDB");
        }

        //Mapping the foreign keys 
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            //one to many
            modelBuilder.Entity<Booking>()
                .HasOne(f => f.Flight)
                .WithMany(b => b.Bookings)
                .HasForeignKey(f => f.FlightId);

            //composite key for m:n
            /*modelBuilder.Entity<BookingPassenger>()
                .HasKey(p => new { p.BookingId, p.PassengerId });*/

            // m : n relation with 2 foreignkey
            modelBuilder.Entity<Flight>()
                .HasOne(p => p.SourceAirport)
                .WithMany(x => x.SourceFlights)
                .HasForeignKey(m => m.SourceAirportId);

            modelBuilder.Entity<Flight>()
                .HasOne(p => p.DestinationAirport)
                .WithMany(p => p.DestinationFlights)
                .HasForeignKey(p => p.DestinationAirportId);
        }
    }
}
