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

namespace MayNazMuth
{
    /// <summary>
    /// Interaction logic for BookingReportWindow1.xaml
    /// </summary>
    public partial class BookingReportWindow1 : Window
    {
        public BookingReportWindow1()
        {
            InitializeComponent();

            ToggleEventHandlers(false);
            populateDataGrid();
            ToggleEventHandlers(true);
        }
    

        private void ToggleEventHandlers(bool toggle)
        {
            if (toggle)
            {
                //turn on               
                FilterButton.Click += searchData;

            }
            else
            {
                //Turn off               

                FilterButton.Click -= searchData;
            }
        }

        public void populateDataGrid()
        {

            //bookingID, flightID, from, to, departureDate, ArrivalDate, Card HolderName, bookingStatus
            using (var db = new CustomDbContext())
            {
                var query = db.Passengers
                              .Join(db.BookingPassengers,
                                    passengers => passengers.PassengerId,
                                    pid => pid.PassengerId,
                                    (passengers, pid) => new {
                                        passengers,
                                        pid
                                    })
                              .Join(db.Bookings,
                                     bookings => bookings.pid.BookingId,
                                     bid => bid.BookingId,
                                     (bookings, bid) => new {
                                         bookings,
                                         bid
                                     })
                              .Join(db.Flights,
                                     flights => flights.bid.FlightId,
                                     fid => fid.FlightId,
                                     (flights, fid) => new {
                                         flights,
                                         fid
                                     })

                              .Select(m => new {
                                  passengerName = m.flights.bookings.passengers.FullName,                                 
                                  passengerPassport = m.flights.bookings.passengers.PassportNo,
                                  flightNumber = m.flights.bid.Flight.FlightNo,
                                  flightDeparture = m.flights.bid.Flight.DepartureTime,
                                  flightArrival = m.flights.bid.Flight.ArrivalTime,
                                  flightDepartureAirport = m.flights.bid.Flight.SourceAirport.AirportName,
                                  flightArrivalAirport = m.flights.bid.Flight.DestinationAirport.AirportName,                                  
                                  bookingDateTime = m.flights.bid.BookingDatetime,
                                  bookingSatus = m.flights.bid.BookingStatus
                              });

                bookingsDataGrid.ItemsSource = query.ToList();
                lblNumberOfBookings.Content = query.ToList().Count.ToString();
            }
        }

        private void searchData(object sender, RoutedEventArgs e)
        {
            lblNumberOfBookings.Content = "";
            var startFrom = fromDatePicker.SelectedDate;
            var endTo = toDatePicker.SelectedDate;

            using (var db = new CustomDbContext())
            {
                var query = db.Passengers
                              .Join(db.BookingPassengers,
                                    passengers => passengers.PassengerId,
                                    pid => pid.PassengerId,
                                    (passengers, pid) => new
                                    {
                                        passengers,
                                        pid
                                    })
                              .Join(db.Bookings,
                                     bookings => bookings.pid.BookingId,
                                     bid => bid.BookingId,
                                     (bookings, bid) => new
                                     {
                                         bookings,
                                         bid
                                     })
                              .Join(db.Flights,
                                     flights => flights.bid.FlightId,
                                     fid => fid.FlightId,
                                     (flights, fid) => new
                                     {
                                         flights,
                                         fid
                                     })

                              .Select(m => new
                              {
                                  passengerName = m.flights.bookings.passengers.FullName,
                                  passengerPassport = m.flights.bookings.passengers.PassportNo,
                                  flightNumber = m.flights.bid.Flight.FlightNo,
                                  flightDeparture = m.flights.bid.Flight.DepartureTime,
                                  flightArrival = m.flights.bid.Flight.ArrivalTime,
                                  flightDepartureAirport = m.flights.bid.Flight.SourceAirport.AirportName,
                                  flightArrivalAirport = m.flights.bid.Flight.DestinationAirport.AirportName,
                                  bookingDateTime = m.flights.bid.BookingDatetime,
                                  bookingSatus = m.flights.bid.BookingStatus
                              });


                if (startFrom <= endTo)
                {
                    var searchResult = query.Where(x => (x.flightDeparture >= startFrom && x.flightDeparture <= endTo));
                    bookingsDataGrid.ItemsSource = searchResult.ToList();

                    lblNumberOfBookings.Content = searchResult.ToList().Count().ToString();

                    
                }
                else
                {
                    MessageBox.Show("Start Date can not be later than End Date");
                }

            }
        }


        private void clearFilters(object sender, EventArgs args)
        {

            fromDatePicker.SelectedDate = null;
            toDatePicker.SelectedDate = null;
            populateDataGrid();
        }




    }
}
