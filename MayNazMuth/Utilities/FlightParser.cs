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

                        Airline airline = new Airline(fields[3]);
                        Airport sourceAirport = new Airport(fields[4]);
                        Airport destinaionsAirport = new Airport(fields[5]);
                        //string nFlightNo, DateTime nDepartureTime, DateTime nArrivalTime,
                        // int nBookingId, Airline nAirline, Airline nSourceAirport,
                        // Airline nDestinationAirport
                        Flight newFlight = new Flight(
                            fields[0],
                            Convert.ToDateTime(fields[1]),
                            Convert.ToDateTime(fields[2]),
                            airline,
                            sourceAirport,
                            destinaionsAirport
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
