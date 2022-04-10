using MayNazMuth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MayNazMuth.Utilities
{
    class AirlineParser
    {
        public static List<Airline> airlineList = new List<Airline>();
        public static List<Airline> parseAirlineFile(String contents)
        {


            //Get the file into Lines
            string[] lines = contents.Split('\n');
            foreach (string line in lines)
            {
                //Cut the line into the fileds
                string[] fields = line.Trim().Split(',');

                if (fields.Length != 1)
                {
                    //MessageBox.Show("Problem parsing file, check format");
                    break;

                }
                else
                {
                    try
                    {                       
                        Airline newAirline = new Airline(
                            fields[0].Trim()                                                      
                            );

                        airlineList.Add(newAirline);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }



            }
           
            return airlineList;
        }
    }
}
