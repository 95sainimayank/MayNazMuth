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
    

        //Method to toggle event handlers
        private void ToggleEventHandlers(bool toggle)
        {
            if (toggle)
            {
                //turn on               
                FilterButton.Click += searchData;
                clearButton.Click += clearFilters;

            }
            else
            {
                //Turn off               

                FilterButton.Click -= searchData;
                clearButton.Click -= clearFilters;
            }
        }



        public void populateDataGrid()
        {

            //Query that joins Passengers, bookings and Flights table to populate data to the booking report
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

                //populare data filtered from the query to the datagrid
                bookingsDataGrid.ItemsSource = query.ToList();

                //get the total number of bookings
                lblNumberOfBookings.Content = query.ToList().Count.ToString();
            }
        }

        //method to filter data
        private void searchData(object sender, RoutedEventArgs e)
        {

            lblNumberOfBookings.Content = "";
            var startFrom = fromDatePicker.SelectedDate;
            var endTo = toDatePicker.SelectedDate;

            //Join Passengers, bookings and Flights table and extract the neccessary column values
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


                //Get booking details based on the given filter 
                //check if start date is less than end date
                if (startFrom <= endTo)
                {
                    var searchResult = query.Where(x => (x.bookingDateTime >= startFrom && x.bookingDateTime <= endTo));
                    bookingsDataGrid.ItemsSource = searchResult.ToList();

                    lblNumberOfBookings.Content = searchResult.ToList().Count().ToString();

                    
                }
                else
                {
                    //show message box if start date is greater than end date
                    MessageBox.Show("Start Date can not be later than End Date");
                }

            }
        }


        //Clear all filters
        private void clearFilters(object sender, EventArgs args)
        {

            fromDatePicker.SelectedDate = DateTime.Now;
            toDatePicker.SelectedDate = DateTime.Now;
            populateDataGrid();
        }




    }
}
