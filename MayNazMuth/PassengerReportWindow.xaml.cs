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

            btnSearch.Click += searchData;
        }

        public void searchData(object sender, EventArgs args) {
            using(var db = new CustomDbContext()) {
                string name = txtPassengerName.Text;
                string contactNo = txtPassengerContact.Text;
                string passport = txtPassengerPassport.Text;
/*
                var h = from f in db.Flights
                        from b in db.Bookings
                        where f.FlightId == b.Flight
*/


                /*var allPassengers = from z in db.Passengers
                    where z.FullName.Contains(name) && z.PassportNo.Contains(passport) && z.PhoneNo.Contains(contactNo)
                    select z;
*/
                /*from s in allPassengers
                from c in db.Bookings
                from x in db.BookingPassengers
                from f in db.Flights
                where s.PassengerId == x.PassengerId && x.BookingId == c.BookingId;
                foreach (Passenger p in allPassengers) {
                    
                    P

                }*/

                db.SaveChanges();
            }
            
        }
    }
}
