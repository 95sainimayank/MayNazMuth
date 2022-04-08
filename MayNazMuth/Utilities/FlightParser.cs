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
        public static List<Flight> flightsList = new List<Flight>();
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

                if (fields.Length != 9)
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
                            Convert.ToInt32(fields[3]),
                             fields[4].Trim(),
                             Convert.ToInt32(fields[5]),
                             fields[6].Trim(),
                             Convert.ToInt32(fields[7]),
                             fields[8].Trim()
                             
                                                          
                            );

                        flightsList.Add(newFlight);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }



            }
           
            return flightsList;
        }
    }
}
