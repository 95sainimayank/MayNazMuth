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

            InitializeDataGrid();
        }

        public void InitializeDataGrid()
        {
            //using (var db = new CustomDbContext())
            //{
            //    var query = db.Bookings
            //                  .Join(db.Flights,
            //                        flights => flights.FlightId,                                  
            //                        (flights, fid) => new {
            //                            flights,
            //                            fid
            //                        })
            //                  .Join(db.Payments,
            //                         bookings => bookings.pid.BookingId,
            //                         bid => bid.BookingId,
            //                         (bookings, bid) => new {
            //                             bookings,
            //                             bid
            //                         })
            //                  .Select(m => new {
            //                      passengerName = m.bookings.passengers.FullName,
            //                      passengerPhone = m.bookings.passengers.PhoneNo,
            //                      passengerPassport = m.bookings.passengers.PassportNo,
            //                      flightArrival = m.bid.Flight.ArrivalTime,
            //                      flightDeparture = m.bid.Flight.DepartureTime,
            //                      flightArrivalAirport = m.bid.Flight.DestinationAirport.AirportName,
            //                      flightDepartureAirport = m.bid.Flight.SourceAirport.AirportName,
            //                      bookingDateTime = m.bid.BookingDatetime,
            //                  });

            //    PassengerReportDatagrid.ItemsSource = query.ToList();
            //}
        }
    }
}
