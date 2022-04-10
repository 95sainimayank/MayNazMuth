using MayNazMuth.Entities;
using MayNazMuth.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MayNazMuth {
    public partial class PassengerReportWindow : Window {
        public PassengerReportWindow() {
            InitializeComponent();

            InitializeDataGrid();

            btnSearch.Click += searchData;
        }

        public void InitializeDataGrid() {
            using(var db = new CustomDbContext()) {
                var query = db.Passengers
                              .Join(db.BookingPassengers,
                                    passengers => passengers.PassengerId,
                                    pid => pid.PassengerId,
                                    (passengers, pid) => new {
                                        passengers, pid
                                    })
                              .Join(db.Bookings,
                                     bookings => bookings.pid.BookingId,
                                     bid => bid.BookingId,
                                     (bookings, bid) => new {
                                         bookings, bid
                                     })
                              .Select(m => new {
                                  passengerName = m.bookings.passengers.FullName,
                                  passengerPhone = m.bookings.passengers.PhoneNo,
                                  passengerPassport = m.bookings.passengers.PassportNo,
                                  flightArrival = m.bid.Flight.ArrivalTime,
                                  flightDeparture = m.bid.Flight.DepartureTime,
                                  flightArrivalAirport = m.bid.Flight.DestinationAirport.AirportName,
                                  flightDepartureAirport = m.bid.Flight.SourceAirport.AirportName,
                                  bookingDateTime = m.bid.BookingDatetime,
                              });

                PassengerReportDatagrid.ItemsSource =  query.ToList();
                Console.WriteLine("----" + query.Count());
            }
        }

        public void searchData(object sender, EventArgs args) {
            using (var db = new CustomDbContext()) {
                string name = txtPassengerName.Text.Trim();
                string contactNo = txtPassengerContact.Text.Trim();
                string passport = txtPassengerPassport.Text.Trim();

                var query = db.Passengers
                              .Join(db.BookingPassengers,
                                    passengers => passengers.PassengerId,
                                    pid => pid.PassengerId,
                                    (passengers, pid) => new {
                                        passengers, pid
                                    })
                              .Join(db.Bookings,
                                     bookings => bookings.pid.BookingId,
                                     bid => bid.BookingId,
                                     (bookings, bid) => new {
                                         bookings, bid
                                     })
                              .Select(m => new {
                                  passengerName = m.bookings.passengers.FullName,
                                  passengerPhone = m.bookings.passengers.PhoneNo,
                                  passengerPassport = m.bookings.passengers.PassportNo,
                                  flightArrival = m.bid.Flight.ArrivalTime,
                                  flightDeparture = m.bid.Flight.DepartureTime,
                                  flightArrivalAirport = m.bid.Flight.DestinationAirport.AirportName,
                                  flightDepartureAirport = m.bid.Flight.SourceAirport.AirportName,
                                  bookingDateTime = m.bid.BookingDatetime,
                              });

                var selectedPassenger = from x in query
                                        where x.passengerName.Contains(name) && x.passengerPassport.Contains(passport) && x.passengerPhone.Contains(contactNo)
                                        select x;

                PassengerReportDatagrid.ItemsSource = selectedPassenger.ToList();
                db.SaveChanges();
            }

        }

        
    }
}
