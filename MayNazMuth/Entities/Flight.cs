using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayNazMuth.Entities {
    class Flight {
        public int FlightId { get; set; }
        public string FlightNo { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        //Navigational Properties
        public List<Booking> Bookings{ get; set; }
        public Airline Airline { get; set; }
        public int? SourceAirportId { get; set; }
        public int? DestinationAirportId { get; set; }
        public Airport SourceAirport { get; set; }
        public Airport DestinationAirport { get; set; }
        
        public Flight() {
            Bookings = new List<Booking>();
        }

        public Flight(string nFlightNo, DateTime nDepartureTime, DateTime nArrivalTime, Airline nAirline, 
            Airport nSourceAirport, Airport nDestinationAirport)
        {
            FlightNo = nFlightNo;
            DepartureTime = nDepartureTime;
            ArrivalTime = nArrivalTime;
            Airline = nAirline;
            SourceAirport = nSourceAirport;
            DestinationAirport = nDestinationAirport;
            
        }
    }
}
