using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayNazMuth.Entities {
    class Flight
    {
        public int FlightId { get; set; }
        public string FlightNo { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public double Price { get; set; }

        //Navigational Properties
        public List<Booking> Bookings { get; set; }
        public Airline Airline { get; set; }
        public string AirlineName { get; set; }
        public string SourceAirportName { get; set; }
        public string DestinationAirportName { get; set; }
        public int? AirlineId { get; set; }
        public int? SourceAirportId { get; set; }
        public int? DestinationAirportId { get; set; }
        public Airport SourceAirport { get; set; }
        public Airport DestinationAirport { get; set; }

        //constructors
        public Flight()
        {
            Bookings = new List<Booking>();
        }

        

        //constructor for Flight class
        public Flight(int nAirlineID,string nNo, DateTime nDepart, DateTime nArrival, string nAirline, 
            string nSourceAirport, string nDestinationAirport,double nPrice,int nSourceApId,int nDestinationApId)
        {
            AirlineId = nAirlineID; 
            AirlineName = new Airline(nAirline).AirlineName;
            SourceAirportName = new Airport(nSourceAirport).AirportName;
            DestinationAirportName = new Airport(nDestinationAirport).AirportName;
            FlightNo = nNo;
            DepartureTime = nDepart;
            ArrivalTime = nArrival;
            Price = nPrice;
            SourceAirportId = nSourceApId;
            DestinationAirportId = nDestinationApId;

        }
    }
}
