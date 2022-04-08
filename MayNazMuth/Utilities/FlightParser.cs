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
        public static List<Flight> flights2 = new List<Flight>();
        public static List<Flight> parseFlightFile(String contents)
        {
            /////List<Flight> flightList = new List<Flight>();
            //Airline airline = new Airline();
            //Airport sAirport = new Airport();
            //Airport dAirport = new Airport();


            //Get the file into Lines
            string[] lines = contents.Split('\n');
            foreach (string line in lines)
            {
                //Cut the line into the fileds

                string[] fields = line.Trim().Split(',');

                if (fields.Length != 6)
                {
                    //MessageBox.Show("Problem parsing file, check format");
                    break;

                }
                else
                {
                    try
                    {                       

                        //string[] myFields = fields[0].Split('\\');
                        //fields[1] = PlayerFields[0];
                        //fields[0] = fields[0].AirlineName
                        Flight newFlight = new Flight(                           

                           
                             fields[0].Trim(),
                            Convert.ToDateTime(fields[1]),
                            Convert.ToDateTime(fields[2]),
                             fields[3].Trim(),
                             fields[4].Trim(),
                             fields[5].Trim()
                            );

                        flights2.Add(newFlight);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }



            }
           
            return flights2;
        }
    }
}
