using MayNazMuth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayNazMuth {
    class Airport {
        public int AirportId { get; set; }
        public string AirportName { get; set; }
        public string AirportAddress { get; set; }
        public string AirportCity { get; set; }
        public string AirportCountry { get; set; }
        public string AirportAbbreviation { get; set; }
        public string AirportEmail { get; set; }
        public string AirportWebsite { get; set; }
        public string AirportPhoneno { get; set; }

        //Navigation properties
        public List<Flight> SourceFlights { get; set; }
        public List<Flight> DestinationFlights { get; set; }


        public Airport() {
            SourceFlights = new List<Flight>();
            DestinationFlights = new List<Flight>();
        }

        public Airport(string nAirportName)
        {
            AirportName = nAirportName;
        }

        public Airport(string nName, string nAddress, string nCity, string nCountry,
            string nAbr, string nEmail, string nWebsite, string nPhone) {
            AirportName = nName;
            AirportAddress = nAddress;
            AirportCity = nCity;
            AirportCountry = nCountry;
            AirportAbbreviation = nAbr;
            AirportEmail = nEmail;
            AirportWebsite = nWebsite;
            AirportPhoneno = nPhone;
        }


    }
}
