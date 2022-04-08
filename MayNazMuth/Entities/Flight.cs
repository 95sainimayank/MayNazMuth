﻿using System;
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
        public int? SourceAirportId { get; set; }
        public int? DestinationAirportId { get; set; }
        public Airport SourceAirport { get; set; }
        public Airport DestinationAirport { get; set; }


        public Flight()
        {
            Bookings = new List<Booking>();
        }

        public Flight(string nNo, DateTime nDepart, DateTime nArrival,string nAirline, string nSourceAirport, string nDestinationAirport)
        {
            Airline = new Airline(nAirline);
            SourceAirport = new Airport(nSourceAirport);
            DestinationAirport = new Airport(nDestinationAirport);
            FlightNo = nNo;
            DepartureTime = nDepart;
            ArrivalTime = nArrival;
           // Airline = new Airline(nAirline);
            Airline.AirlineName = nAirline;
            SourceAirport.AirportName = nSourceAirport;
            DestinationAirport.AirportName = nDestinationAirport;


        }
    }
}
