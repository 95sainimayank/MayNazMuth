using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayNazMuth.Entities {
    class BookingPassenger {
        public int BookingPassengerId { get; set; }
        public int BookingId { get; set; }
        public int PassengerId { get; set; }


        public Booking Booking { get; set; }
        public Passenger Passenger { get; set; }
    }
}
