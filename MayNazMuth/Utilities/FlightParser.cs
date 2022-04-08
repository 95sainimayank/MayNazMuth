using MayNazMuth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MayNazMuth.Utilities
{
    class FlightParser
    {
        public static List<Flight> parseFlightFile(string contents)
        {
            List<Flight> flightList = new List<Flight>();
            //Airline airline = new Airline();
            //Airport sAirport = new Airport();
            //Airport dAirport = new Airport();


            //Get the file into Lines
            string[] lines = contents.Split('\n');

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(',');

                if (fields.Length != 6)
                {
                    continue;
                }
                else
                {
                    try
                    {

                        //Sanitize the data
                        //for (int f = 0; f < fields.Length; f++)
                        //{
                        //    fields[f] = (fields[f]).Trim();
                        //}

                        //Airline airline;
                        //Airport sourceAirport;
                        //Airport destinaionsAirport;
                        //string nFlightNo, DateTime nDepartureTime, DateTime nArrivalTime,
                        // int nBookingId, Airline nAirline, Airline nSourceAirport,
                        // Airline nDestinationAirport
                        Flight newFlight = new Flight(
                            fields[0],
                            Convert.ToDateTime(fields[1]),
                            Convert.ToDateTime(fields[2]),
                             fields[3],
                             fields[4],
                             fields[5]

                         );
                        

                        flightList.Add(newFlight);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Problem parsing file at line " + (i + 1));
                        Console.WriteLine(ex.ToString());
                    }


                }

            }
            return flightList;
        }
    }
}
