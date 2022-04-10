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


            //Get the file into Lines
            string[] lines = contents.Split('\n');
            foreach (string line in lines)
            {
                //Cut the line into the fileds
                string[] fields = line.Trim().Split(',');

                if (fields.Length != 10)
                {
                    //MessageBox.Show("Problem parsing file, check format");
                    continue;

                }
                else
                {
                    try
                    {                       
                        Flight newFlight = new Flight(                           
                             Convert.ToInt32(fields[0]),
                             fields[1].Trim(),
                             Convert.ToDateTime(fields[2]),
                             Convert.ToDateTime(fields[3]),
                             fields[4].Trim(),
                             fields[5].Trim(),
                             fields[6].Trim(),
                             Convert.ToDouble(fields[7]),
                             Convert.ToInt32(fields[8]),
                             Convert.ToInt32(fields[9])
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
