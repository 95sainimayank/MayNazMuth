using MayNazMuth.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayNazMuth.Utilities {
    class CustomDbContext: DbContext {
        public DbSet<Booking> Bookings{ get; set; }
        public DbSet<Flight> Flights{ get; set; }
        public DbSet<Payment> Payments{ get; set; }
        public DbSet<Airline> Airlines { get; set; }
        
        public DbSet<Airport> Airports { get; set; }
        
        public DbSet<Passenger> Passengers{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MayNazMuthDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            //for composite key
            modelBuilder.Entity<BookingPassenger>()
                .HasKey(p => new {p.BookingId, p.PassengerId });

            //for one to many
            modelBuilder.Entity<Airline>()
                .HasMany(p => p.Flights)
                .WithOne(p => p.Airline);

            //for m : n relation
            modelBuilder.Entity<BookingPassenger>()
                .HasOne(p => p.Booking)
                .WithMany(p => p.BookingPassengers)
                .HasForeignKey(p => p.BookingId);

            modelBuilder.Entity<BookingPassenger>()
                .HasOne(p => p.Passenger)
                .WithMany(p => p.BookingPassengers)
                .HasForeignKey(p => p.PassengerId);

            /*modelBuilder.Entity<Booking>()
                .HasOne(a => a.Payment)
                .WithOne(b => b.Booking)
                .HasForeignKey<Payment>(c => c.PaymentId);

            modelBuilder.Entity<Booking>()
                .HasOne(a => a.Flight)
                .WithOne(b => b.Booking)
                .HasForeignKey<Flight>(c => c.FlightId);*/
        }
    }
}
