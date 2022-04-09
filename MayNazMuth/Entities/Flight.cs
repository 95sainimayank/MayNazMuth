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


        public Flight()
        {
            Bookings = new List<Booking>();
        }

        

        public Flight(string nNo, DateTime nDepart, DateTime nArrival, int nAirlineId,string nAirline, 
            int nSourceAirportID,string nSourceAirport, int nDestinationAIrportID, string nDestinationAirport)
        {
            AirlineName = new Airline(nAirline).AirlineName;
            SourceAirportName = new Airport(nSourceAirport).AirportName;
            DestinationAirportName = new Airport(nDestinationAirport).AirportName;
            //SourceAirport = new Airport();
            //DestinationAirport = new Airport();
            //SourceAirportId = new Airport(nSourceAirport).AirportId;
            //DestinationAirportId = new Airport(nDestinationAirport).AirportId;
            FlightNo = nNo;
            DepartureTime = nDepart;
            ArrivalTime = nArrival;
            // Airline = new Airline(nAirline);
            //Airline.AirlineName = nAirline;
            this.AirlineId = nAirlineId;
            this.SourceAirportId = nSourceAirportID;
            this.DestinationAirportId = nDestinationAIrportID;


        }
    }
}
