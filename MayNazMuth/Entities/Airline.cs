using MayNazMuth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayNazMuth {
    class Airline {
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }

        //navigational properties
        public List<Flight> Flights{ get; set; }

        //Constructors
        public Airline() {

        }

        public Airline(string nName) {
            AirlineName = nName;
        }
    }
}
