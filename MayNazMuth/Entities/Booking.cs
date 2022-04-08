using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayNazMuth.Entities {
    class Booking {
        public int BookingId { get; set; }
        public DateTime BookingDatetime { get; set; }
        public string BookingStatus { get; set; }

        //navigational properties
        public int FlightId { get; set; }
        public Flight Flight { get; set;}
        public List<BookingPassenger> BookingPassengers { get; set; }

        public Booking() {
            BookingPassengers = new List<BookingPassenger>();
        }

        public Booking(DateTime nDT, string nStatus) {
            BookingDatetime = nDT;
            BookingStatus = nStatus;
        }
    }
}
